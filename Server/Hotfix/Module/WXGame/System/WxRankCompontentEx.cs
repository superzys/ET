using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;

namespace ETHotfix
{

    [ObjectSystem]
    public class WxRankCompontentAwakeSystem : AwakeSystem<WxRankCompontent,long >
    {
        public override void Awake(WxRankCompontent self,long type)
        {
            self.Awake(type);
        }
    }
    public static class WxRankCompontentEx
    {
        public static async void Awake(this WxRankCompontent self,long type)
        {
            //此处应当从数据库读取数据
            RankInfo rankInfo = await RankInfoFactory.GetOrCreate(type);
            self.rankInfo = rankInfo;
            self.InitRankInfo(rankInfo);

        }
        /// <summary>
        /// 初始化 排行组件;
        /// </summary>
        /// <param name="self"></param>
        /// <param name="rankInfo"></param>
        public static void InitRankInfo(this WxRankCompontent self, RankInfo rankInfo)
        {
            //整理门槛
            if (self.rankInfo.RankPlayerArr.Count > 0)
            {
                self.DoorValue = self.rankInfo.RankPlayerArr[self.rankInfo.RankPlayerArr.Count - 1].Value;
            }
            else
            {
                self.DoorValue = 10;
            }
            //把玩家的排行映射做掉
            self.playerRankArr = new Dictionary<long, long>();
            self.saveplayerIDArr = new List<long>();

            for (int i = 0; i < self.rankInfo.RankPlayerArr.Count; i++)
            {
                self.playerRankArr.Add(self.rankInfo.RankPlayerArr[i].UserId, i);
                self.saveplayerIDArr.Add(self.rankInfo.RankPlayerArr[i].UserId);
            }
        }
        /// <summary>
        /// 有玩家数据更新了
        /// </summary>
        /// <param name="self"></param>
        /// <param name="userInfo"></param>
        public static void UpdataOneUserData(this WxRankCompontent self, WxRankObj userInfo)
        {
            if (self.rankInfo.RankPlayerArr.Count < 100)
            {
                if (userInfo.Value > self.DoorValue)
                {
                    self.InsertOnePlayerToRank(userInfo);
                }
            }
            else
            {
                if (userInfo.Value > self.DoorValue)
                {
                    self.AddOnePlayToWaiteTeam(userInfo);
                }
            }
        }
        /// <summary>
        /// 强行插入当前队列
        /// </summary>
        /// <param name="self"></param>
        /// <param name="userInfo"></param>
         static void InsertOnePlayerToRank(this WxRankCompontent self, WxRankObj userInfo)
        {
            if (self.saveplayerIDArr.IndexOf(userInfo.UserId) < 0)
            {
                int insertIndex = 0;
                for (int i = 0; i < self.rankInfo.RankPlayerArr.Count; i++)
                {
                    WxRankObj rankObj = self.rankInfo.RankPlayerArr[i];
                    if (rankObj.Value < userInfo.Value)
                    {
                        insertIndex = i;
                        break;
                    }
                }
                self.rankInfo.RankPlayerArr.Insert(0, userInfo);
                //更新后面的人的排序
                for (int i = insertIndex; i < self.rankInfo.RankPlayerArr.Count; i++)
                {
                    WxRankObj rankObj = self.rankInfo.RankPlayerArr[i];
                    rankObj.RankNum = i;
                }
                self.saveplayerIDArr.Add(userInfo.UserId);
            }


        }
        /// <summary>
        ///添加到等待队列;
        /// </summary>
        /// <param name="self"></param>
        /// <param name="userInfo"></param>
         static void AddOnePlayToWaiteTeam(this WxRankCompontent self, WxRankObj userInfo)
        {
            if (self.saveplayerIDArr.IndexOf(userInfo.UserId) < 0)
            {
                self.rankInfo.CatchPlayerArr.Add(userInfo);
                self.saveplayerIDArr.Add(userInfo.UserId);
            }

        }
        /// <summary>
        /// 组件里的玩家重新排序;
        /// </summary>
        /// <param name="self"></param>
        public static void ReRankPlayer(this WxRankCompontent self)
        {
            if(self.rankInfo.CatchPlayerArr.Count>0)
            {
                List<WxRankObj> newArr = self.rankInfo.CatchPlayerArr.OrderBy(u => u.Value).ToList();
                int valueNew = newArr[0].Value;
                int insertIdx = -1;
                for (int i = 0; i < self.rankInfo.RankPlayerArr.Count; i++)
                {
                    if (self.rankInfo.RankPlayerArr[i].Value < valueNew)//从这里开始插
                    {
                        insertIdx = i;
                        break;
                    }
                }
                if (insertIdx == -1)
                {
                    insertIdx = self.rankInfo.RankPlayerArr.Count;
                }
                //新的插进去
                self.rankInfo.RankPlayerArr.InsertRange(insertIdx, newArr);
                //删除多的
                if (self.rankInfo.RankPlayerArr.Count > 100)
                {
                    self.rankInfo.RankPlayerArr.RemoveRange(100, self.rankInfo.RankPlayerArr.Count - 100);
                }
                self.rankInfo.CatchPlayerArr.Clear();
            }

        }
        /// <summary>
        /// 取得排行的数量
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<RankUserInfoNet> GetRankPlayerArr(this WxRankCompontent self)
        {
            List<RankUserInfoNet> arr = new List<RankUserInfoNet>();
            for (int i = 0; i < self.rankInfo.RankPlayerArr.Count; i++)
            {
                 WxRankObj rankObj = self.rankInfo.RankPlayerArr[i];
                arr.Add(rankObj.GetNetObj());
                if (i>=99)
                {
                    break;
                }

            }
            return arr;
        }

    }
}
