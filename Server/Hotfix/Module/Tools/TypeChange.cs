using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class TypeChange
    {
        public static long TurnStringTolong(string str)
        {
            long getId = 0;
            try
            {
                if (str != null && str != "")
                {
                    getId = long.Parse(str);
                }
            }
            catch (Exception e)
            {
                getId = 0;
            }
            return getId;
        }
    }
}
