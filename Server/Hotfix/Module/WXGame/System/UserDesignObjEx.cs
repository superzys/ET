using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class UserDesignObjAwakeSystem : AwakeSystem<UserDesignObj>
    {
        public override void Awake(UserDesignObj self)
        {
            self.Awake();
        }
    }


    public static class UserDesignObjEx
    {
        public static void Awake(this UserDesignObj self)
        {
        

        }

        public static void SetWxDesignObj(this UserDesignObj self, WxDesignReqNet wxInfo)
        {
            if (wxInfo != null)
            {
                self.LeftPhoto = wxInfo.LeftPhoto;
                self.RightPhoto = wxInfo.RightPhoto;
                self.Status = 1;
                self.WordsArr = wxInfo.WordsArr;
                self.TipsArr = wxInfo.TipsArr;
            }
        }
    }
}
