using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public static class RankInfoFactory
    {
        /// <summary>
        /// 定好了各个类型排行的id
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<RankInfo> GetOrCreate(long type)
        {
            //查询用户信息
            DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
            RankInfo rankInfo = await dbProxyComponent.Query<RankInfo>(type, true);
            if (rankInfo == null) //没有这个排行信息
            {
                rankInfo = ComponentFactory.CreateWithId<RankInfo>(type);
                rankInfo.CatchPlayerArr = new List<WxRankObj>();
                rankInfo.RankPlayerArr = new List<WxRankObj>();

                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                await dbProxy.Save(rankInfo, true);
            }
            return rankInfo;
        }
    }
}
