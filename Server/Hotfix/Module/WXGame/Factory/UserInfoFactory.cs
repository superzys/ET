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
            List<ComponentWithId>  userArr =  await dbProxyComponent.Query<UserInfo>($"{{OpenId:'{wxInfo.openId}'}}");
            if (userArr == null || userArr.Count == 0) //没有这个用户
            {
                UserInfo newUser = ComponentFactory.Create<UserInfo>();
                newUser.NickName = wxInfo.nickName;
                newUser.OpenId = wxInfo.openId;
                newUser.Gender = wxInfo.gender;
                newUser.City = wxInfo.city;
                newUser.Country = wxInfo.country;
                newUser.Province = wxInfo.province;
                newUser.AvatarUrl = wxInfo.avatarUrl;
                newUser.UnionId = wxInfo.unionId;
                userInfo = newUser;
                await dbProxyComponent.Save(newUser, false);
            }
            else
            {
                userInfo = userArr[0] as UserInfo;
            }
            return userInfo;
        }
    }
}
