using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 游戏者身上带个计时器组件，太久了就清除掉
    /// </summary>
    public  class WxGamerTimerComponent:Entity
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
