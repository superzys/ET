using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
 
    [HttpHandler(AppType.Gate, "/")]
    public class RankController : AHttpHandler
    {
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> GetRankInfo(WxGetRankReqNet wxInfo)
        {
            try
            {

                long sessionID = 0;
                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                {
                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                }
                UserInfo userInfo = null;
                if (sessionID > 0)
                {
                    WxUserMangerComponent wxUserManger = Game.Scene.GetComponent<WxUserMangerComponent>();
                    //能取到之前的用户的话
                    WxGamer player = wxUserManger.Get(sessionID);
                    userInfo = player.GetComponent<UserInfo>();
                    if (userInfo != null)
                    {
                        WxGetRankResNet resNet = new WxGetRankResNet();
                        resNet.RankArr = new List<RankUserInfoNet>();
                        RankUserInfoNet oneUser = new RankUserInfoNet()
                        {
                            rankNum = 1,
                            nickName = userInfo.NickName,
                            avatarUrl = userInfo.AvatarUrl,
                            PveNum = userInfo.GameInfo.PlotIdArr.Count,
                            ChapterId = userInfo.GameInfo.ChapterId+"",
                            PlotId =  userInfo.GameInfo.PlotId+""
                        };
                        resNet.RankArr.Add(oneUser);
                        return Ok(resNet.ToJson());
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
