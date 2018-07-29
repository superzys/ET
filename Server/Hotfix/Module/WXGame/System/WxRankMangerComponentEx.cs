using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{

    [ObjectSystem]
    public class WxRankMangerComponentSystem : AwakeSystem<WxRankMangerComponent>
    {
        public override void Awake(WxRankMangerComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 存下来所有的排行组件
    /// </summary>
    public static class WxRankMangerComponentEx
    {
        public static void Awake(this WxRankMangerComponent self)
        {
            self.allRankCmpArr = new List<WxRankCompontent>();

            self.AddComponent<WxRankTimerComponent>();
            WxRankCompontent gameRank = ComponentFactory.Create<WxRankCompontent, long>((long)WxRankType.GameRank);
            WxRankCompontent wujinRank = ComponentFactory.Create<WxRankCompontent, long>((long)WxRankType.WujinRank);
            WxRankCompontent chutiRank = ComponentFactory.Create<WxRankCompontent, long>((long)WxRankType.ChutiRank);

            self.allRankCmpArr.Add(gameRank);
            self.allRankCmpArr.Add(wujinRank);
            self.allRankCmpArr.Add(chutiRank);
        }

        public static List<RankUserInfoNet> GetRankPlayerArrInMag(this WxRankMangerComponent self, int rankType)
        {
            if (self.allRankCmpArr.Count > rankType)
            {
                WxRankCompontent rankCmp = self.allRankCmpArr[rankType];
                if (rankCmp != null)
                {
                    List<RankUserInfoNet> arr = rankCmp.GetRankPlayerArr();
                    return arr;
                }
            }
            return new List<RankUserInfoNet>();
        }

        /// <summary>
        /// 玩家的关卡信息变化
        /// </summary>
        /// <param name="self"></param>
        public static void UpdataOneUserPlotInfo(this WxRankMangerComponent self, UserInfo userInfo)
        {
            try
            {
                int ranktype = (int) WxRankType.GameRank;
                
                WxRankCompontent rankCmp = self.allRankCmpArr[ranktype];
                WxRankObj rankObj = userInfo.GetRankObj();
                rankCmp.UpdataOneUserData(rankObj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        /// <summary>
        /// 玩家的无尽信息变化
        /// </summary>
        /// <param name="self"></param>
        public static void UpdataOneUserWuJinInfo(this WxRankMangerComponent self, UserInfo userInfo)
        {
            WxRankCompontent rankCmp = self.allRankCmpArr[(int)WxRankType.WujinRank];
            WxRankObj rankObj = userInfo.GetWuJinRankObj();
            rankCmp.UpdataOneUserData(rankObj);
        }
        /// <summary>
        /// 玩家的出题数量变化
        /// </summary>
        /// <param name="self"></param>
        public static void UpdataOneUserCreateIssueInfo(this WxRankMangerComponent self, UserInfo userInfo)
        {
            WxRankCompontent rankCmp = self.allRankCmpArr[(int)WxRankType.ChutiRank];
            WxRankObj rankObj = userInfo.GetChuTiRankObj();
            rankCmp.UpdataOneUserData(rankObj);
        }
    }
}
