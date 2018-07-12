using System;
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
        public async Task<HttpResult> LoginWx(WxLoginReqNet wxInfo)
        {
            try
            {
                //有userid 并且可转换long
                long userID = 0;
                if (wxInfo.UserId != null && wxInfo.UserId != "")
                {
                    userID = TypeChange.TurnStringTolong(wxInfo.UserId);
                }
                UserInfo userInfo = null;
                if (userID > 0)
                {
                    //能取到之前的用户的话
                    userInfo = await UserInfoFactory.GetOrCreate(userID);
                    if (userInfo != null)
                    {
                        UserInfoFactory.OneUserOnLine(userInfo);

                        WxLoginResNet resNet = userInfo.GetLoginResNetObj();

                        return Ok(resNet.ToJson());
                    }
                }
                //没有的话只能 走微信验证拿到openId 了
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
                //去数据库取或者创建
                userInfo = await UserInfoFactory.GetOrCreate(wxUserInfo);

                UserInfoFactory.OneUserOnLine(userInfo);
                
                WxLoginResNet reNet = userInfo.GetLoginResNetObj();

                return Ok(reNet.ToJson());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

            //            return Ok("登陆成功！");
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