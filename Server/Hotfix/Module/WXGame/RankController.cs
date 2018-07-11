using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
 
    [HttpHandler(AppType.Gate, "/")]
    public class RankController : AHttpHandler
    {
        [Post] // url-> /GainPlotReward
        public async Task<HttpResult> GetRankInfo(WxGetRankReqNet wxInfo)
        {
            return Ok("登陆成功！");
        }
    }
}
