using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class testWords : MonoBehaviour {


    //一个问题
    class OneIssue
    {
        public int _id { get; set; }//
        public string Name { set; get; }
        public List<OneDialog> DialogsArr { set; get; }//对话的数组
        public List<string> RightAnsArr { set; get; }//正确答案
        public List<string> WrongAnsArr { set; get; }//干扰项
        public int OptionNum { get; set; }//选择文字个数   
        public int RewardGoldNum { get; set; }//破关奖励
    }
    //一个对话内容
    class OneDialog
    {
        public int PhotoId { set; get; }//发言者头像Id
        public bool IsLeft { set; get; }//对话框背景
        public string Words { set; get; }//说的话
        public List<int> ImgFaceArr { set; get; }//表情
    }

    // Use this for initialization
    void Start () {
        ShowWords();
    }

    void ShowWords()
    {
        OneIssue oneIssue = new OneIssue();
        oneIssue.Name = "名字";
        oneIssue.OptionNum = 6;
        oneIssue.RightAnsArr = new List<string>();
        oneIssue.RightAnsArr.Add("答");
        oneIssue.RightAnsArr.Add("案");
        oneIssue.WrongAnsArr = new List<string>();
        oneIssue.WrongAnsArr.Add("错");
        oneIssue.WrongAnsArr.Add("误");
        oneIssue.WrongAnsArr.Add("干");
        oneIssue.WrongAnsArr.Add("扰");
        oneIssue.DialogsArr = new List<OneDialog>();
        oneIssue.DialogsArr.Add(new OneDialog()
        {
            PhotoId = 0,
            IsLeft = true,
            Words = "请问你"
        });
        oneIssue.DialogsArr.Add(new OneDialog()
        {
            PhotoId = 1,
            IsLeft = false,
            Words = "你问撒"
        });
        oneIssue.DialogsArr.Add(new OneDialog()
        {
            PhotoId = 0,
            IsLeft = true,
            Words = "问完了"
        });
        string listJson = JsonMapper.ToJson(oneIssue);
        Debug.Log(listJson);

        OneIssue pers = JsonMapper.ToObject<OneIssue>(listJson);
        Debug.Log("name" + pers.Name);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
