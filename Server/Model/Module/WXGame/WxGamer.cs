using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    ///微信玩家
    /// </summary>
    public class WxGamer :Entity
    {
        public bool IsNeedCatch { get; set; }
        public long LastAliveTime { get; set; }
        public long LastSaveTime { get; set; }
    }
}
