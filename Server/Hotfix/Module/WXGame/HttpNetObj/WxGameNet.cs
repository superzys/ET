using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    #region 游戏中
    /// <summary>
    /// 破关
    /// </summary>
    public class WxGainPlotRewardReqNet
    {
        public string SessonId { get; set; }
        public string PlotId { get; set; }
        public string ChapterId { get; set; }
    }
    public class WxGainPlotRewardResNet
    {
        public int RewardGold { get; set; }
        public string UserGoldNum { get; set; }
    }
    /// <summary>
    /// 解锁新章节
    /// </summary>
    public class WxGainChapterRewardReqNet
    {
        public string SessonId { get; set; }
        public string PlotId { get; set; }
        public string ChapterId { get; set; }
    }
    public class WxGainChapterRewardResNet
    {
        public bool IsSuccess { get; set; }
        public int RewardGold { get; set; }
        public string UserGoldNum { get; set; }
    }
    /// <summary>
    /// 消耗提示
    /// </summary>
    public class WxCostTipReqNet
    {
        public string SessonId { get; set; }
        public string PlotId { get; set; }
    }
    public class WxCostTipResNet
    {
        public int CostGold { get; set; }
        public string UserGoldNum { get; set; }
    }

    #endregion


    public class WxShareOnceReqNet
    {
        public string SessonId { get; set; }
    }
    public class WxShareOnceResNet
    {
        public int RewardGold { get; set; }
        public string UserGoldNum { get; set; }
    }

}
