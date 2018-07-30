using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class WxRankObj
    {
        public int RankNum { get; set; }
        public long UserId { get; set; }
        public string AvatarUrl { get; set; }

        public string NickName { get; set; }

        //排行进度    不同的排行不一样
        public int Value { get; set; }
        public int Value1 { get; set; }


        //排行进度  最新的   不同的排行不一样
        public int LastValue { get; set; }
        public int LastValue1 { get; set; }
    }
}
