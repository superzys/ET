using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public class WxGetRankReqNet
    {
        public string SessonId { get; set; }
        public int RankType { get; set; }
    }
    public class WxGetRankResNet
    {
        public List<RankUserInfoNet> RankArr { get; set; }
    }

    public class RankUserInfoNet
    {
        public int rankNum { get; set; }
        public string nickName { get; set; }
        public string avatarUrl { get; set; }
        public int value { get; set; }
        //        public string ChapterId { get; set; }
        //        public string PlotId { get; set; }
        //        public int PveNum { get; set; }
    }
}

