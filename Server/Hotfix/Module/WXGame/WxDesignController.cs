using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [HttpHandler(AppType.Gate, "/")]
    public class WxDesignController : AHttpHandler
    {
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> SendDesignIssue(WxDesignReqNet wxInfo)
        {
            try
            {
                long sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);

                UserInfo userInfo = null;
                if (sessionID > 0)
                {
                    WxUserMangerComponent wxUserManger = Game.Scene.GetComponent<WxUserMangerComponent>();
                    //能取到之前的用户的话
                    WxGamer player = wxUserManger.Get(sessionID);
                    userInfo = player.GetComponent<UserInfo>();
                    if (userInfo != null)
                    {
                        UserDesignObj designObj = ComponentFactory.Create<UserDesignObj>();

                        designObj.SetWxDesignObj(wxInfo);

                        userInfo.DesignArr.Add(designObj);

                        player.IsNeedCatch = true;

                        //告诉排行榜组件 更新
                        WxRankMangerComponent wxRankCmp = Game.Scene.GetComponent<WxRankMangerComponent>();
                        wxRankCmp.UpdataOneUserCreateIssueInfo(userInfo);

                        WxDesignResNet resNet = new WxDesignResNet();
                        resNet.Status = 1;
                        return Ok(resNet.ToJson());

                    }
                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("\"error\" ");

            }
        }
    }
}
