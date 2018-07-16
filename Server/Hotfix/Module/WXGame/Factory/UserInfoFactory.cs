using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public static class UserInfoFactory
    {
        public static async Task<UserInfo> GetOrCreate(WechatUserInfo wxInfo)
        {
            //查询用户信息
            DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
            UserInfo userInfo = null;
            List<ComponentWithId> userArr = await dbProxyComponent.Query<UserInfo>($"{{OpenId:'{wxInfo.openId}'}}");
            if (userArr == null || userArr.Count == 0) //没有这个用户
            {
                userInfo = ComponentFactory.Create<UserInfo>();

                userInfo.NickName = wxInfo.nickName;
                userInfo.OpenId = wxInfo.openId;
                userInfo.Gender = wxInfo.gender;
                userInfo.City = wxInfo.city;
                userInfo.Country = wxInfo.country;
                userInfo.Province = wxInfo.province;
                userInfo.AvatarUrl = wxInfo.avatarUrl;
                userInfo.UnionId = wxInfo.unionId;

                userInfo.GameInfo = ComponentFactory.Create<GameInfoObj>();
                userInfo.GameOpArr = new List<GameOpObj>();
                userInfo.DesignArr = new List<UserDesignObj>();
                userInfo.GameInfo.ChapterId = 1;

                ChapterData chapterData = (ChapterData)Game.Scene.GetComponent<ConfigComponent>()
                    .Get(typeof(ChapterData), (int)1);
                userInfo.GameInfo.PlotId = chapterData.PlotIDArr[0];
                userInfo.GameInfo.PlotIndex = 0;

                userInfo.GameInfo.PlotIdArr = new List<int>();
                //                userInfo = userInfo;
                await dbProxyComponent.Save(userInfo, false);
            }
            else
            {
                userInfo = userArr[0] as UserInfo;
            }
            return userInfo;
        }

        public static async Task<UserInfo> GetOrCreate(long userId)
        {
            //查询用户信息
            DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
            UserInfo userInfo = await dbProxyComponent.Query<UserInfo>(userId, true);

            return userInfo;
        }
        /// <summary>
        /// 一个用户上线了
        /// 把玩家数据挂载
        /// 挂载计时器
        /// </summary>
        /// <param name="userInfo"></param>
        public static void OneUserOnLine(UserInfo userInfo)
        {
            WxGamer gamer = ComponentFactory.CreateWithId<WxGamer>(userInfo.InstanceId);
            gamer.LastAliveTime = TimeHelper.ClientNowSeconds();
            gamer.IsNeedCatch = false;
            userInfo.FixCheckOnLogin();

            gamer.AddComponent(userInfo);

            gamer.AddComponent<WxGamerTimerComponent>();

            Game.Scene.GetComponent<WxUserMangerComponent>().Add(gamer, userInfo.Id);
        }

    }
}
