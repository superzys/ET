using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public class LoginDemo
    {

        [Get] // url-> /PushInfo?name=11&age=1111
        public string PushInfo(string name, int age, HttpListenerRequest req, HttpListenerResponse resp)
        {
            Log.Info(name);
            Log.Info($"{age}");
            return "ok";
        }

        [Get("t")] // url-> /t
        public int Test()
        {
            System.Console.WriteLine("");
            return 1;
        }

        [Post] // url-> /Test1
        public object Test1(HttpListenerRequest req)
        {
            return new
            {

            };
        }

        [Get] // url-> /Test2
        public int Test2(HttpListenerResponse resp)
        {
            return 1;
        }

//        [Get] // url-> /GetRechargeRecord
//        public async Task<HttpResult> GetRechargeRecord(long id)
//        {
//            // var db = Game.Scene.GetComponent<DBProxyComponent>();
//
//            // var info = await db.Query<RechargeRecord>(id);
//
//            await Task.Delay(1000); // 用于测试
//
//            object info = null;
//            if (info != null)
//            {
//                return Ok(data: info);
//            }
//            else
//            {
//                return Error("ID不存在！");
//            }
//        }
    }
}
