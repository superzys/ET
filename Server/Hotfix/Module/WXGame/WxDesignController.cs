using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

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


                    }
                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                return Ok("\"error\" ");
                Console.WriteLine(e);

            }
        }
    }
}
