using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public static class ChapterDataEx
    {
        public static int GetPlotIndex(this ChapterData self,long plotId)
        {
            for (int  i=0;i<self.PlotIDArr.Length;i++)
            {
                if (self.PlotIDArr[i] == plotId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
