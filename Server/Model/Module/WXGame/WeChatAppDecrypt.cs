using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace ETModel
{
    /// <summary>
    /// 处理微信小程序用户数据的签名验证和解密
    /// </summary>
    public class WeChatAppDecrypt : Component
    {
        public string appId { get; set; }
        public string appSecret { get; set; }




    }
    /// <summary>
    /// 白鹭的会啊直接发code 和用户信息。 
    /// </summary>
    public class WechatLoginInfoEgret
    {
        public string code { get; set; }
        public string nickName { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
    }

    /// <summary>
    /// 微信小程序登录信息结构
    /// </summary>
    public class WechatLoginInfo
    {
        public string code { get; set; }
        public string encryptedData { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
    }
    /// <summary>
    /// 微信小程序用户信息结构
    /// </summary>
    public class WechatUserInfo
    {
        public string openId { get; set; }
        public string session_key { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
        public Watermark watermark { get; set; }

        public class Watermark
        {
            public string appid { get; set; }
            public string timestamp { get; set; }
        }
    }
    /// <summary>
    /// 微信小程序从服务端获取的OpenId和SessionKey信息结构
    /// </summary>
    public class OpenIdAndSessionKey
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}