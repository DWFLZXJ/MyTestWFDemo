using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestWF
{
    class JieMi
    {

        public static string JiaMiValue(string strValue)
        {
            string skey = "hLmziwNncGGqqsFwuAlQIVRXR8FFQx1b";
            string JIAMI_Value = Class_JM_JYZX.code_JYZX.DeTransform1_JYZX(strValue, skey);
            return JIAMI_Value;
        }

        public static string JieMiValue(string strValue)
        {
            string skey = "hLmziwNncGGqqsFwuAlQIVRXR8FFQx1b";
            string JIEMI_Value = Class_JM_JYZX.code_JYZX.Transform1_JYZX(strValue, skey);
            return JIEMI_Value;
        }


    }
}

