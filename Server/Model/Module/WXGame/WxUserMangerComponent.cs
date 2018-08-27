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
        /// <summary>
        /// 是sessionId 坐下标
        /// WxGamer.id  是sessionId
        /// </summary>
        protected readonly  Dictionary<long, WxGamer> wxUserArr = new Dictionary<long, WxGamer>();
        /// <summary>
        /// 用户session 对比
        /// </summary>
        protected readonly Dictionary<long, long> wxUserSessionArr = new Dictionary<long, long>();

        public void Awake()
        {
            Instance = this;
        }

        public void Add(WxGamer player,long userid)
        {
            if (!this.wxUserArr.ContainsKey(player.Id))
            {
                this.wxUserArr.Add(player.Id, player);
            }
            else
            {
                this.wxUserArr[player.Id] = player;
            }

            if (!this.wxUserSessionArr.ContainsKey(userid))
            {
                this.wxUserSessionArr.Add(userid, player.Id);
            }
            else
            {
                this.wxUserSessionArr[userid] = player.Id;
            }
        }
        public long GetUserSessionId(long id)
        {
            this.wxUserSessionArr.TryGetValue(id, out long  sessionId);
            return sessionId;
        }

        public WxGamer GetByUserId(long id)
        {
            this.wxUserSessionArr.TryGetValue(id, out long SessionId);
            if (SessionId > 0)
            {
                this.wxUserArr.TryGetValue(id, out WxGamer gamer);
                return gamer;
            }

            return null;
        }

        public WxGamer GetBySessionId(long id)
        {
            this.wxUserArr.TryGetValue(id, out WxGamer gamer);
            return gamer;
        }

        public void RemoveBySessionId(long id)
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
