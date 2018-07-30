using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    //0 普通排行
    //1 无尽
    //2 出题
    [BsonIgnoreExtraElements]
    public class RankInfo : Entity
    {
        public  int LastSaveDay { get; set; }
        public List<WxRankObj> RankPlayerArr = new List<WxRankObj>();
        public List<WxRankObj> CatchPlayerArr = new List<WxRankObj>();
    }
}
