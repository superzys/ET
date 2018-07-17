using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    /// <summary>
    /// 玩家出个题
    /// </summary>
    public class WxDesignReqNet
    {
        public string SessonId { get; set; }

        public int LeftPhoto { get; set; }
        public int RightPhoto { get; set; }

        public List<string> WordsArr { get; set; }
        //玩家的答案
        public List<string> TipsArr { get; set; }

    }

    public class WxDesignResNet
    {
        public int Status { get; set; }
    }
}