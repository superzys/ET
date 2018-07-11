using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 游戏数据
    /// </summary>
    public class GameInfoObj : Entity
    {
        public long Gold { get; set; }
        public int SignDayNum { get; set; }
        public int RemainSignNumToday { get; set; }

        public long FirstLoginTime { get; set; }

        public int LastLoginDay { get; set; }
        public int LastSignDay { get; set; }
        public long LastSignTime { get; set; }

        public int ShareNumToday { get; set; }


        public long ChapterId { get; set; }
        public long PlotIndex { get; set; }
        public long PlotId { get; set; }

        //打过的id数组。 这个可以用做进度
        public List<long>  PlotIdArr { get; set; }
    }
}
