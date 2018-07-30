using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class WxRankTimerComponentAwakeSystem : AwakeSystem<WxRankTimerComponent>
    {
        public override void Awake(WxRankTimerComponent self)
        {
            self.Awake();
        }
    }
    /// <summary>
    /// 定时存排行榜数据
    /// </summary>
    public static class WxRankTimerComponentEx
    {
        public static async void Awake(this WxRankTimerComponent self)
        {
            self.timerComponent = Game.Scene.GetComponent<TimerComponent>();
            self.IsTimeing = true;
            self.StartPlayerTimer();

        }

        public static async void StartPlayerTimer(this WxRankTimerComponent self)
        {
            try
            {
                WxRankMangerComponent rankMngCmp = self.Entity as WxRankMangerComponent;

                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                DateTime curTime = new DateTime();
                //开启计时器
                while (self.IsTimeing)
                {
                    await self.timerComponent.WaitAsync(1000);
                    if (rankMngCmp != null)//每秒 
                    {
                        for (int i = 0; i < rankMngCmp.allRankCmpArr.Count; i++)
                        {
                            WxRankCompontent rankCmp = rankMngCmp.allRankCmpArr[i];
                            if (TimeHelper.ClientNowSeconds() - rankCmp.LastSaveTime > 300)//需要重新排行
                            {
                                 curTime = new DateTime();
                                if (rankCmp.RankType == (long) WxRankType.GameRank) //只有游戏排行五分钟拍一次
                                {
                                    rankCmp.rankInfo.LastSaveDay = curTime.Day;
                                    rankCmp.ReRankPlayer();
                                }
                                else
                                {
                                    if (rankCmp.rankInfo.LastSaveDay != curTime.Day && curTime.Hour >=3)
                                    {
                                        rankCmp.rankInfo.LastSaveDay = curTime.Day;
                                        rankCmp.ReRankPlayer();
                                    }
                                }
                                //所有的排行都要存入数据库/
                                rankCmp.LastSaveTime = TimeHelper.ClientNowSeconds();
                                await dbProxy.Save(rankCmp.rankInfo, true);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Log.Info(e.ToString());

            }
           
        }
    }
}
