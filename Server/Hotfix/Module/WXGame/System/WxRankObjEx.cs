using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
  public static  class WxRankObjEx
    {
        public static RankUserInfoNet GetNetObj(this WxRankObj self)
        {
            RankUserInfoNet netObj = new RankUserInfoNet()
            {
                rankNum = self.RankNum,
                nickName = self.NickName,
                avatarUrl = self.AvatarUrl,
                value = self.Value,
                value1 = self.Value1
            };

            return netObj;
        }
    }
}
