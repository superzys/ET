using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 管理微信登录上来的用户的组件
    /// </summary>
    public class WxUserMangerComponent :Entity
    {
        public static WxUserMangerComponent Instance { get; private set; }

        protected  readonly  Dictionary<long, WxGamer> wxUserArr = new Dictionary<long, WxGamer>();

        public void Awake()
        {
            Instance = this;
        }

        public void Add(WxGamer player)
        {
            if (!this.wxUserArr.ContainsKey(player.Id))
            {
                this.wxUserArr.Add(player.Id, player);
            }
        }

        public WxGamer Get(long id)
        {
            this.wxUserArr.TryGetValue(id, out WxGamer gamer);
            return gamer;
        }

        public void Remove(long id)
        {
            this.wxUserArr.Remove(id);
        }

        public int Count
        {
            get
            {
                return this.wxUserArr.Count;
            }
        }

        public WxGamer[] GetAll()
        {
            return this.wxUserArr.Values.ToArray();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (WxGamer player in this.wxUserArr.Values)
            {
                player.Dispose();
            }

            Instance = null;
        }
    }
}
