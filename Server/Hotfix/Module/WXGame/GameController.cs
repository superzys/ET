using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpx;
using ETModel;
using MongoDB.Bson;

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
                        if (userInfo.GameInfo.RemainSignNumToday > 0)
                        {
                            UnitConfig data = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1);
                            //因为配置表可能更新了
                            if (userInfo.GameInfo.SignDayNum >= data.SignRewardArr.Length)
                            {
                                userInfo.GameInfo.SignDayNum = data.SignRewardArr.Length - 1;
                            }

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

                long plotId = sessionID = TypeChange.TurnStringTolong(wxInfo.PlotId);
                long ChapterId = sessionID = TypeChange.TurnStringTolong(wxInfo.ChapterId);

                UserInfo userInfo = null;
                if (sessionID > 0 && plotId > 0)
                {
                    WxUserMangerComponent wxUserManger = Game.Scene.GetComponent<WxUserMangerComponent>();
                    //能取到之前的用户的话
                    WxGamer player = wxUserManger.Get(sessionID);
                    userInfo = player.GetComponent<UserInfo>();
                    if (userInfo != null)
                    {
                        //没打过的 并且确实是当前进度的章节
                        if (userInfo.GameInfo.PlotIdArr.IndexOf(plotId) < 0 && userInfo.GameInfo.ChapterId == ChapterId)
                        {
                            IssueData data = (IssueData)Game.Scene.GetComponent<ConfigComponent>()
                                .Get(typeof(IssueData), (int)plotId);
                            ChapterData chapterData = (ChapterData)Game.Scene.GetComponent<ConfigComponent>()
                                .Get(typeof(ChapterData), (int)ChapterId);
                            if (chapterData.PlotIDArr.Contains(plotId))
                            {
                                userInfo.GameInfo.PlotIdArr.Add(plotId);
                                WxGainPlotRewardResNet resNet = new WxGainPlotRewardResNet();

                                userInfo.GameInfo.Gold += data.RewardGoldNum;
                                resNet.RewardGold = data.RewardGoldNum;
                                resNet.UserGoldNum = userInfo.GameInfo.Gold.ToString();

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

                long curPlotId = sessionID = TypeChange.TurnStringTolong(wxInfo.PlotId);
                long ChapterId = sessionID = TypeChange.TurnStringTolong(wxInfo.ChapterId);

                UserInfo userInfo = null;
                if (sessionID > 0)
                {
                    WxUserMangerComponent wxUserManger = Game.Scene.GetComponent<WxUserMangerComponent>();
                    //能取到之前的用户的话
                    WxGamer player = wxUserManger.Get(sessionID);
                    userInfo = player.GetComponent<UserInfo>();
                    if (userInfo != null)
                    {
                        if (userInfo.GameInfo.ChapterId == ChapterId)
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
                                long plotId = chapterData.PlotIDArr[i];
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
                                //                                ChapterData nextChapterData = (ChapterData)Game.Scene.GetComponent<ConfigComponent>()
                                //                                    .Get(typeof(ChapterData), (int)userInfo.GameInfo.ChapterId);
                                resNet.IsSuccess = true;

                            }
                            else
                            {
                                resNet.IsSuccess = false;
                            }
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
