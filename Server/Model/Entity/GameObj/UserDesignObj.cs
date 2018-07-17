using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{

    /// <summary>
    /// 玩家设计的题目
    /// </summary>
    public class UserDesignObj :Entity
    {
        public int LeftPhoto { get; set; }
        public int RightPhoto { get; set; }

        public List<string> WordsArr { get; set; }

        public List<string> TipsArr { get; set; }
        //1 审核中，2 通过
        public int Status { get; set; }
    }
}
