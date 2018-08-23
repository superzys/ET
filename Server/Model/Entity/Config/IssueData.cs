using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ETModel.Config(ETModel.AppType.Client)]
    public partial class EventDataCategory : ACategory<IssueData>
    {
    }
    public class IssueData : IConfig
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }
        public string Name { set; get; }
        public List<OneDialog> DialogsArr { set; get; }//对话的数组
        public List<string> RightAnsArr { set; get; }//正确答案
        public List<string> WrongAnsArr { set; get; }//干扰项
        public int OptionNum { get; set; }//选择文字个数   
        public int RewardGoldNum { get; set; }//破关奖励
        public int TipCostGold { get; set; }//提示消耗 
        public string AnsDes { set; get; }
    }
    //一个对话内容
    public class OneDialog
    {
        public int PhotoId { set; get; }//发言者头像Id
        public bool IsLeft { set; get; }//对话框背景
        public string Words { set; get; }//说的话
        public List<int> ImgFaceArr { set; get; }//表情
    }
}
