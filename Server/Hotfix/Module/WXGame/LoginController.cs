using System.Net;
using ETModel;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace ETHotfix
{
    [HttpHandler(AppType.Gate, "/")]
    public class LoginController : AHttpHandler
    {
        [Post] // url-> /Login
        public async Task<HttpResult> LoginWx(WxLoginNet wxInfo)
        {
            WechatLoginInfoEgret wxLoginInfo = new WechatLoginInfoEgret();
            ReflexCopyData.CopyEntityToObj(wxLoginInfo, wxInfo);

            WeChatAppDecrypt wxDecCmp = Game.Scene.GetComponent<WeChatAppDecrypt>();
            //            OpenIdAndSessionKey sessionInfo = wxDecCmp.GetWechatUserSessionInfo(wxLoginInfo);
            OpenIdAndSessionKey sessionInfo = new OpenIdAndSessionKey()
            {
                openid = "12343214",
                session_key = "32324324324"
            };
            WechatUserInfo wxUserInfo = new WechatUserInfo();
            ReflexCopyData.CopyEntityToObj(wxUserInfo, wxInfo);
            wxUserInfo.openId = sessionInfo.openid;
            wxUserInfo.session_key = sessionInfo.session_key;

            UserInfo userInfo = await UserInfoFactory.GetOrCreate(wxUserInfo);
            WxLoginReNet reNet = new WxLoginReNet();
            
            reNet.UserId = userInfo.Id.ToString();

            //            return Ok(msg:"post wx done", data: userInfo);

                        return Ok(reNet.ToJson());
//            return Ok("登陆成功！");
        }
        /// <summary>
        /// 登录已有账号
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        public async Task<HttpResult> LoginWxWithAccount(WxLoginAccNet wxInfo)
        {
            UserInfo userInfo = await UserInfoFactory.GetOrCreate(wxInfo.UserId);
            WxLoginReNet reNet = new WxLoginReNet();

            reNet.UserId = userInfo.Id.ToString();


            return Ok(reNet.ToJson());
        }

        [Post] // url-> /Login
        public object Login(Account account)
        {
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            string bsonStr = account.ToJson(jsonWriterSettings);

            string str1 = account.ToJson();
            Account acc = JsonHelper.FromJson<Account>(bsonStr);
            Account acc1 = JsonHelper.FromJson<Account>(str1);

            return Ok("登陆成功！");
        }

    }
}