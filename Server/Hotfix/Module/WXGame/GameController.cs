using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [HttpHandler(AppType.Gate, "/")]
    public class GameController : AHttpHandler
    {
        /// <summary>
        /// 领取登录奖励
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainLoginReward
        public async Task<HttpResult> GainLoginReward(WxLoginRewardReqNet wxInfo)
        {
            try
            {
                return Ok("Ok ");
            }
            catch (Exception e)
            {
                return Ok("error ");
                Console.WriteLine(e);
            }
         
        }

        /// <summary>
        /// 领取破关奖励
        /// 破关后进度更新
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> GainPlotReward(WxGainPlotRewardReqNet wxInfo)
        {
            try
            {
                
             return Ok("登陆成功！");
            }
            catch (Exception e)
            {
                return Ok("error ");
                Console.WriteLine(e);
            
            }
        }
        /// <summary>
        /// 领取章节奖励
        /// 更新进度
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainChapterReward
        public async Task<HttpResult> GainChapterReward(WxGainChapterRewardReqNet wxInfo)
        {
            return Ok("登陆成功！");
        }

        /// <summary>
        /// 分享了一次
        /// </summary>
        /// <param name="wxInfo"></param>
        /// <returns></returns>
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> ShareOnce(WxShareOnceReqNet wxInfo)
        {
            return Ok("登陆成功！");
        }
    }
}
