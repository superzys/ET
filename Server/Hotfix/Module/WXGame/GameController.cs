using ETModel;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpx;

namespace ETHotfix
{
    [HttpHandler(AppType.Gate, "/")]
    public class GameController : AHttpHandler
    {
        /// <summary>
        /// 领取登录奖励
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> GainLoginReward(WxLoginRewardReqNet wxInfo)
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
                        if (userInfo.GameInfo.RemainSignNumToday > 0 || true)
                        {
                            UnitConfig data = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1);
                            //因为配置表可能更新了
                            if (userInfo.GameInfo.SignDayNum >= data.SignRewardArr.Length)
                            {
                                userInfo.GameInfo.SignDayNum = data.SignRewardArr.Length - 1;
                            }

                            userInfo.GameInfo.LastSignDay = TimeHelper.GetDay();
                            userInfo.GameInfo.LastSignTime = TimeHelper.ClientNow();
                            WxLoginRewardResNet resNet = new WxLoginRewardResNet();
                            userInfo.GameInfo.RemainSignNumToday--;
                            resNet.AddGoldNum = data.SignRewardArr[userInfo.GameInfo.SignDayNum];
                            userInfo.GameInfo.SignDayNum++;
                            userInfo.GameInfo.Gold += resNet.AddGoldNum;
                            resNet.UserGoldNum = userInfo.GameInfo.Gold.ToString();

                            if (userInfo.GameInfo.SignDayNum >= data.SignRewardArr.Length)
                            {
                                userInfo.GameInfo.SignDayNum = data.SignRewardArr.Length - 1;
                            }
                            player.IsNeedCatch = true;
                            return Ok(resNet.ToJson());
                        }
                        else
                        {
                            return Ok("{\"error\":2}");
                        }

                    }
                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        /// <summary>
        /// 领取破关奖励
        /// 破关后进度更新
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> GainPlotReward(WxGainPlotRewardReqNet wxInfo)
        {
            try
            {
                long sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);

                int plotId  = (int)TypeChange.TurnStringTolong(wxInfo.PlotId);
                long ChapterId =  TypeChange.TurnStringTolong(wxInfo.ChapterId);

                UserInfo userInfo = null;
                if (sessionID > 0 && plotId > 0)
                {
                    WxUserMangerComponent wxUserManger = Game.Scene.GetComponent<WxUserMangerComponent>();
                    //能取到之前的用户的话
                    WxGamer player = wxUserManger.Get(sessionID);
                    if (player != null)
                    {
                        userInfo = player.GetComponent<UserInfo>();
                        if (userInfo != null)
                        {
                            //没打过的 并且确实是当前进度的章节
//                      章节可重复不能此检测      userInfo.GameInfo.PlotIdArr.IndexOf(plotId) < 0 &&
                            if (userInfo.GameInfo .PlotId == plotId && userInfo.GameInfo.ChapterId == ChapterId || true)
                            {
                                IssueData data = (IssueData)Game.Scene.GetComponent<ConfigComponent>()
                                    .Get(typeof(IssueData), (int)plotId);
                                ChapterData chapterData = (ChapterData)Game.Scene.GetComponent<ConfigComponent>()
                                    .Get(typeof(ChapterData), (int)ChapterId);
                                if (chapterData.PlotIDArr.Contains(plotId))
                                {
                                    int curIndex = chapterData.GetPlotIndex(plotId);
                                    if (curIndex < (chapterData.PlotIDArr.Length -1))
                                    {
                                        userInfo.GameInfo.PlotId = chapterData.PlotIDArr[curIndex + 1];
                                        userInfo.GameInfo.PlotIndex = curIndex + 1;
                                    }

                                    userInfo.GameInfo.PlotIdArr.Add(plotId);

                                    //告诉排行榜组件 更新
                                    WxRankMangerComponent wxRankCmp = Game.Scene.GetComponent<WxRankMangerComponent>();
                                    wxRankCmp.UpdataOneUserPlotInfo(userInfo);

                                    WxGainPlotRewardResNet resNet = new WxGainPlotRewardResNet();
                                    Log.Info($"Gain PLot reward {ChapterId} {plotId}");
                                    userInfo.GameInfo.Gold += data.RewardGoldNum;
                                    resNet.RewardGold = data.RewardGoldNum;
                                    resNet.UserGoldNum = userInfo.GameInfo.Gold.ToString();
                                    player.IsNeedCatch = true;
                                    return Ok(resNet.ToJson());
                                }
                                else
                                {
                                    return Ok("{\"error\":3}");
                                }
                            }
                            else
                            {
                                return Ok("{\"error\":2}");
                            }
                        }
                    }
                   
                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");

            }
        }
        /// <summary>
        /// 领取章节奖励
        /// 更新进度
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainChapterReward
        public async Task<HttpResult> GainChapterReward(WxGainChapterRewardReqNet wxInfo)
        {
            try
            {
                long sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);

                int curPlotId =  (int)TypeChange.TurnStringTolong(wxInfo.PlotId);
                long ChapterId =  TypeChange.TurnStringTolong(wxInfo.ChapterId);

                UserInfo userInfo = null;
                if (sessionID > 0)
                {
                    WxUserMangerComponent wxUserManger = Game.Scene.GetComponent<WxUserMangerComponent>();
                    //能取到之前的用户的话
                    WxGamer player = wxUserManger.Get(sessionID);
                    userInfo = player.GetComponent<UserInfo>();
                    if (userInfo != null)
                    {
                        if (userInfo.GameInfo.ChapterId == ChapterId || true)
                        {
                            IssueData data = (IssueData)Game.Scene.GetComponent<ConfigComponent>()
                                .Get(typeof(IssueData), (int)curPlotId);
                            WxGainChapterRewardResNet resNet = new WxGainChapterRewardResNet();
                            if (userInfo.GameInfo.PlotIdArr.IndexOf(curPlotId) < 0)
                            {
                                userInfo.GameInfo.PlotIdArr.Add(curPlotId);
                                resNet.RewardGold = data.RewardGoldNum;
                                userInfo.GameInfo.Gold += data.RewardGoldNum;
                            }

                            ChapterData chapterData = (ChapterData)Game.Scene.GetComponent<ConfigComponent>()
                                .Get(typeof(ChapterData), (int)ChapterId);
                            bool isAllDone = true;
                            for (int i = 0; i < chapterData.PlotIDArr.Length; i++)
                            {
                                int plotId = (int)chapterData.PlotIDArr[i];
                                if (userInfo.GameInfo.PlotIdArr.IndexOf(plotId) < 0)
                                {
                                    isAllDone = false;
                                    break;
                                }
                            }
                            resNet.UserGoldNum = userInfo.GameInfo.Gold.ToString();

                            if (isAllDone) //通关此章节
                            {
                                userInfo.GameInfo.ChapterId++;
                                 ChapterData nextChapterData = (ChapterData)Game.Scene.GetComponent<ConfigComponent>()
                                                                    .Get(typeof(ChapterData), (int)userInfo.GameInfo.ChapterId);
                                if (nextChapterData != null)
                                {
                                    userInfo.GameInfo.PlotId = nextChapterData.PlotIDArr[0];
                                    userInfo.GameInfo.PlotIndex = 0;
                                }
                                resNet.IsSuccess = true;

                            }
                            else
                            {
                                resNet.IsSuccess = false;
                            }

                            //告诉排行榜组件 更新
                            WxRankMangerComponent wxRankCmp = Game.Scene.GetComponent<WxRankMangerComponent>();
                            wxRankCmp.UpdataOneUserPlotInfo(userInfo);

                            resNet.PlotId = userInfo.GameInfo.PlotId.ToString();
                            Log.Info($"jump chapter {ChapterId} {resNet.PlotId } ");
                            player.IsNeedCatch = true;
                            return Ok(resNet.ToJson());
                        }
                        return Ok("{\"error\":2}");
                    }
                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");

            }
        }

        /// <summary>
        /// 分享了一次
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> ShareOnce(WxShareOnceReqNet wxInfo)
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
                        WxShareOnceResNet resNet = new WxShareOnceResNet();
                        
                        UnitConfig data = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1);
                        if (userInfo .GameInfo.ShareNumToday < data.ShareMaxNum)
                        {
                            userInfo.GameInfo.Gold += data.ShareRewardNum;
                            resNet.RewardGold = data.ShareRewardNum;
                            resNet.UserGoldNum = userInfo.GameInfo.Gold.ToString();
                        }
                        userInfo.GameInfo.ShareNumToday++;
                        player.IsNeedCatch = true;
                        return Ok(resNet.ToJson());
                    }
                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");

            }
        }
    }
}
