using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public class WxLoginNet
    {
        public string code { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
    }
    public class WxLoginAccNet
    {
        public long UserId { get; set; }
    }
    public class WxLoginReNet
    {
        public string UserId { get; set; }
    }
}
