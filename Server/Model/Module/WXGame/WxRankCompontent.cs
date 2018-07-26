using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 排行组件。 此组件数据进入数据库;
    /// 有玩家数据变化维护此组件;
    /// </summary>
    public class WxRankCompontent : Entity
    {
        public  long LastSaveTime { get; set; }
        public long RankType { get; set; }
        public int DoorValue { set; get; } = 10;
        /// <summary>
        /// 存储的排行数据和队列数据
        /// </summary>
        public RankInfo rankInfo { set; get; }

        /// <summary>
        /// 已知玩家 的排行信息;
        /// </summary>
        public Dictionary<long, long> playerRankArr { set; get; }

        /// <summary>
        /// 已经包含了的玩家
        /// </summary>
        public List<long> saveplayerIDArr { set; get; }

        //        /// <summary>
        //        /// 分数达标 等待队列
        //        /// </summary>
        //        public List<WxRankObj> waitePlayerArr = new List<WxRankObj>();
        //        public List<WxRankObj> rankPlayerArr = new List<WxRankObj>();
    }
}
