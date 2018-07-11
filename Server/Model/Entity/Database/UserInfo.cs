using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [BsonIgnoreExtraElements]
    public class UserInfo : Entity
    {
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public int Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string AvatarUrl { get; set; }
        public string UnionId { get; set; }


    }
}
