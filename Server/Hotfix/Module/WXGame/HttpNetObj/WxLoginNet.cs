using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    #region 登录

    public class WxLoginReqNet
    {
        public string UserId { get; set; }
        public string code { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
    }

    public class WxLoginResNet
    {
        //给客户端一个sessonId 做验证吧
        public string SessonId { get; set; }
        public string UserId { get; set; }
        public string ChapterId { get; set; }
//        public int PlotIndex { get; set; }
        public string PlotId { get; set; }
        public List<int> LoginRewardArr { get; set; }
        public int SignedNum { get; set; }
        public bool IsSignedToday { get; set; }
        public int ShareTodayNum { get; set; }

    }


    #endregion

    #region 签到

    public class WxLoginRewardReqNet
    {
        public string SessonId { get; set; }

    }

    public class WxLoginRewardResNet
    {
        public int AddGoldNum { get; set; }
        public string UserGoldNum { get; set; }

    }

    #endregion

}
