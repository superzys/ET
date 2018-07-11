using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 玩家答题记录
    /// </summary>
    public class GameOpObj 
    {
        public  long Plotid { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int TipNum { get; set; }
    }
}
