using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class WeChatAppDecryptAwakeSystem : AwakeSystem<WeChatAppDecrypt>
    {
        public override void Awake(WeChatAppDecrypt self)
        {
            self.Awake();
        }
    }


    public static class WeChatAppDecryptEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId">应用程序的AppId</param>
        /// <param name="appSecret">应用程序的AppSecret</param>
        public static void Awake(this WeChatAppDecrypt self)
        {
            self.appId = "wxc796a64f6bd2ef40";
            self.appSecret = "9d78be461e790dd2518746fc3f83528a";
        }


        /// <summary>
        /// 获取OpenId和SessionKey的Json数据包
        /// </summary>
        /// <param name="code">客户端发来的code</param>
        /// <returns>Json数据包</returns>
        private static string GetOpenIdAndSessionKeyString(this WeChatAppDecrypt self, string code)
        {
            string temp = "https://api.weixin.qq.com/sns/jscode2session?" +
                "appid=" + self.appId
                + "&secret=" + self.appSecret
                + "&js_code=" + code
                + "&grant_type=authorization_code";

            return self.GetResponse(temp);

        }

        /// <summary>
        /// 反序列化包含OpenId和SessionKey的Json数据包
        /// </summary>
        /// <param name="code">Json数据包</param>
        /// <returns>包含OpenId和SessionKey的类</returns>
        public static OpenIdAndSessionKey DecodeOpenIdAndSessionKey(this WeChatAppDecrypt self, string codeStr)
        {
            string ssStr = self.GetOpenIdAndSessionKeyString(codeStr);
            OpenIdAndSessionKey oiask = JsonHelper.FromJson<OpenIdAndSessionKey>(ssStr);
            if (!String.IsNullOrEmpty(oiask.errcode))
                return null;
            return oiask;
        }


        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetResponse(this WeChatAppDecrypt self, string url)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        #region 解密验证
        /// <summary>
        /// 根据微信小程序平台提供的签名验证算法验证用户发来的数据是否有效
        /// </summary>
        /// <param name="rawData">公开的用户资料</param>
        /// <param name="signature">公开资料携带的签名信息</param>
        /// <param name="sessionKey">从服务端获取的SessionKey</param>
        /// <returns>True：资料有效，False：资料无效</returns>
        public static bool VaildateUserInfo(this WeChatAppDecrypt self, string rawData, string signature, string sessionKey)
        {
            //创建SHA1签名类
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            //编码用于SHA1验证的源数据
            byte[] source = Encoding.UTF8.GetBytes(rawData + sessionKey);
            //生成签名
            byte[] target = sha1.ComputeHash(source);
            //转化为string类型，注意此处转化后是中间带短横杠的大写字母，需要剔除横杠转小写字母
            string result = BitConverter.ToString(target).Replace("-", "").ToLower();
            //比对，输出验证结果
            return signature == result;
        }

        /// <summary>
        /// 根据微信小程序平台提供的签名验证算法验证用户发来的数据是否有效
        /// </summary>
        /// <param name="loginInfo">登陆信息</param>
        /// <param name="sessionKey">从服务端获取的SessionKey</param>
        /// <returns>True：资料有效，False：资料无效</returns>
        public static bool VaildateUserInfo(this WeChatAppDecrypt self, WechatLoginInfo loginInfo, string sessionKey)
        {
            return self.VaildateUserInfo(loginInfo.rawData, loginInfo.signature, sessionKey);
        }

        /// <summary>
        /// 根据微信小程序平台提供的签名验证算法验证用户发来的数据是否有效
        /// </summary>
        /// <param name="loginInfo">登陆信息</param>
        /// <param name="idAndKey">包含OpenId和SessionKey的类</param>
        /// <returns>True：资料有效，False：资料无效</returns>
        public static bool VaildateUserInfo(this WeChatAppDecrypt self, WechatLoginInfo loginInfo, OpenIdAndSessionKey idAndKey)
        {
            return self.VaildateUserInfo(loginInfo, idAndKey.session_key);
        }

        /// <summary>
        /// 根据微信小程序平台提供的解密算法解密数据
        /// </summary>
        /// <param name="encryptedData">加密数据</param>
        /// <param name="iv">初始向量</param>
        /// <param name="sessionKey">从服务端获取的SessionKey</param>
        /// <returns></returns>
        public static WechatUserInfo Decrypt(this WeChatAppDecrypt self, string encryptedData, string iv, string sessionKey)
        {
            WechatUserInfo userInfo;
            //创建解密器生成工具实例
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            //设置解密器参数
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            //格式化待处理字符串
            byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
            byte[] byte_iv = Convert.FromBase64String(iv);
            byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);

            aes.IV = byte_iv;
            aes.Key = byte_sessionKey;
            //根据设置好的数据生成解密器实例
            ICryptoTransform transform = aes.CreateDecryptor();

            //解密
            byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

            //生成结果
            string result = Encoding.UTF8.GetString(final);

            //反序列化结果，生成用户信息实例
            //            userInfo = JsonConvert.DeserializeObject<WechatUserInfo>(result);
            userInfo = JsonHelper.FromJson<WechatUserInfo>(result);
            return userInfo;

        }

        /// <summary>
        /// 根据微信小程序平台提供的解密算法解密数据，推荐直接使用此方法
        /// </summary>
        /// <param name="loginInfo">登陆信息</param>
        /// <returns>用户信息</returns>
        public static WechatUserInfo Decrypt(this WeChatAppDecrypt self, WechatLoginInfo loginInfo)
        {
            if (loginInfo == null)
                return null;

            if (String.IsNullOrEmpty(loginInfo.code))
                return null;

            OpenIdAndSessionKey oiask = self.DecodeOpenIdAndSessionKey(loginInfo.code);

            if (oiask == null)
                return null;

            if (!self.VaildateUserInfo(loginInfo, oiask))
                return null;

            WechatUserInfo userInfo = self.Decrypt(loginInfo.encryptedData, loginInfo.iv, oiask.session_key);

            return userInfo;
        }



        #endregion
        /// <summary>
        /// 获取微信用户的session 情况
        /// </summary>
        /// <param name="self"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public static OpenIdAndSessionKey GetWechatUserSessionInfo(this WeChatAppDecrypt self, WechatLoginInfoEgret loginInfo)
        {
            if (loginInfo == null)
                return null;

            if (String.IsNullOrEmpty(loginInfo.code))
                return null;

            OpenIdAndSessionKey oiask = self.DecodeOpenIdAndSessionKey(loginInfo.code);

            if (oiask == null)
                return null;
            return oiask;
        }
    }
}
