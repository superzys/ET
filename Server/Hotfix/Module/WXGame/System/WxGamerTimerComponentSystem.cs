using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class WxGamerTimerComponentAwakeSystem : AwakeSystem<WxGamerTimerComponent>
    {
        public override void Awake(WxGamerTimerComponent self)
        {
            self.Awake();
        }
    }
    /// <summary>
    /// 定时存玩家数据
    /// </summary>
    public static class WxGamerTimerComponentSystem
    {
        public static void Awake(this WxGamerTimerComponent self)
        {
            self.timerComponent = Game.Scene.GetComponent<TimerComponent>();
            self.IsTimeing = true;
            self.StartPlayerTimer();

        }
        public static async void StartPlayerTimer(this WxGamerTimerComponent self)
        {
            try
            {
                WxGamer player = self.Entity as WxGamer;
                UserInfo userInfo = self.Entity.GetComponent<UserInfo>();

                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                //这里竟然蹦过。 
                //开启计时器
                while (self.IsTimeing)
                {
                    await self.timerComponent.WaitAsync(1000);
                    if (userInfo != null)//每秒要检测下将领信息
                    {
                        //                    userInfo.HerosIdArr;
                        if (TimeHelper.ClientNowSeconds() - player.LastSaveTime > 10)//需要存储数据
                        {
                            if (player.IsNeedCatch)
                            {
                                player.IsNeedCatch = false;
                                player.LastSaveTime = TimeHelper.ClientNowSeconds();
                                await dbProxy.Save(userInfo, true);
                            }
                        }
                        if (TimeHelper.ClientNowSeconds() - player.LastAliveTime > 500) //需要移出去
                        {
                            self.IsTimeing = false;
                            player.LastSaveTime = TimeHelper.ClientNowSeconds();
                            await dbProxy.Save(userInfo, false);
                            Game.Scene.GetComponent<WxUserMangerComponent>().Remove(player.Id);
                            player.Dispose();
                            return;
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
