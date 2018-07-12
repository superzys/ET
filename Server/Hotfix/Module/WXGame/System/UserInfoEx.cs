using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public static  class UserInfoEx
    {
        public static WxLoginResNet GetLoginResNetObj(this UserInfo self)
        {
            UnitConfig data = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1);
            WxLoginResNet resNet = new WxLoginResNet()
            {
                UserId = self.Id.ToString(),
                SessonId = self.InstanceId.ToString(),
                Gold = self.GameInfo.Gold.ToString(),
                ChapterId = self.GameInfo.ChapterId.ToString(),
                PlotId = self.GameInfo.PlotId.ToString(),
                LoginRewardArr = new List<int>(data.SignRewardArr),
                SignedNum = self.GameInfo.SignDayNum,
                RemainSignNumToday = self.GameInfo.RemainSignNumToday,
                ShareTodayNum = self.GameInfo.ShareNumToday

            };
            return resNet;
        }
        /// <summary>
        /// 登录的时候数据监测
        /// </summary>
        /// <param name="self"></param>
        public static void FixCheckOnLogin(this UserInfo self)
        {
            if(self.GameInfo == null)
            {
                self.GameInfo = ComponentFactory.Create<GameInfoObj>();
            }
            if (self.GameOpArr == null)
            {
                self.GameOpArr = new List<GameOpObj>();
            }
            if (self.DesignArr == null)
            {
                self.DesignArr = new List<UserDesignObj>();
            }
            int curDay = TimeHelper.GetDay();
            //今天首次登录
            if (self.GameInfo.LastLoginDay != curDay)
            {
                self.GameInfo.ShareNumToday = 0;
                self.GameInfo.RemainSignNumToday = 1;

                self.GameInfo.LastLoginDay = curDay;
            }

        }
    }
}
