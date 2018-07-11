using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{

    /// <summary>
    /// 玩家设计的题目
    /// </summary>
    public class UserDesignObj
    {
        public int LeftPhoto { get; set; }
        public int RightPhoto { get; set; }

        public List<string> WordsArr { get; set; }

        public List<string> TipsArr { get; set; }
        //0 审核中，1 通过
        public int Status { get; set; }
    }
}
