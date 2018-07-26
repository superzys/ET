using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class WxRankTimerComponent : Entity
    {
        /// <summary>
        /// player 销毁时关闭
        /// </summary>
        public bool IsTimeing { set; get; }
        public TimerComponent timerComponent;

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            IsTimeing = false;
            base.Dispose();
        }
    }
}
