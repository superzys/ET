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
    }
}
