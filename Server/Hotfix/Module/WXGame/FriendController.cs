using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    public class AddLotteryTimesClass
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public int dataKey { get; set; }
    }

    public class AddFriendClass
    {
        public int gameId { get; set; }
        public string openId1 { get; set; }
        public string openId2 { get; set; }
    }
    public class setDataClass
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string show { get; set; }
        public string dataKey { get; set; }
        public int dataValue { get; set; }
    }

    public class getMyQuestionReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
    }
    public class payMoneyReq
    {
        public int gameId { get; set; }
        public string code { get; set; }
        public string body { get; set; }
        public string fee { get; set; }    
    }
    public class getDataReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public int dataKey { get; set; }
    }
    public class addQuestionReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public int show { get; set; }
    }
    public class saveDataReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string dataKey { get; set; }
        public string dataValue { get; set; }
    }
    public class lotteryReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string dataKey { get; set; }
        public string show { get; set; }
    }
    public class getMyLotteryReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string dataKey { get; set; }
    }
    public class getQuestionReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string questionId { get; set; }
    }
    public class praiseQuestionReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string questionId { get; set; }
    }
    public class praiseRankReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
    }


    public class getWorldReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
        public string dataKey { get; set; }
    }

    public class getQuestionRankReq
    {
        public int gameId { get; set; }
        public string openId { get; set; }
    }
    

   [HttpHandler(AppType.Gate, "/")]
    public class FriendController : AHttpHandler
    {
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> getQuestion(getQuestionReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> AddFriend(AddFriendClass wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> setData(setDataClass wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> addLotteryTimes(AddLotteryTimesClass wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> getLotteryTimes(AddLotteryTimesClass wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }


        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> getTime()
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> linkEvent()
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> getMyQuestion(getMyQuestionReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> payMoney(payMoneyReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> getData(getDataReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> getFriend(getDataReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> addQuestion(addQuestionReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> saveData(saveDataReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> lottery(lotteryReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> getMyLottery(getMyLotteryReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Get] // url-> /GainLoginReward
        public async Task<HttpResult> praiseQuestion(praiseQuestionReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> praiseRank(praiseRankReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> getWorld(getWorldReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> getQuestionRank(getQuestionRankReq wxInfo)
        {
            try
            {
                //                long sessionID = 0;
                //                if (wxInfo.SessonId != null && wxInfo.SessonId != "")
                //                {
                //                    sessionID = TypeChange.TurnStringTolong(wxInfo.SessonId);
                //                }
                //                UserInfo userInfo = null;
                //                if (sessionID > 0)
                //                {
                //                 
                //                }
                return Ok("{\"error\":1}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok("{\"error\":0}");
            }

        }

    }
}
