using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    /// <summary>
    /// 玩家出的题目单独存
    /// 暂时没有用 直接存userinfo 里了
    /// </summary>
    [BsonIgnoreExtraElements]
    public  class UserIssueInfo : Entity
    {

    }
}
