using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisohEditorWinUI3Blank.DataManagement
{
    public class ConvertHelper
    {
        public static string ObjToStr(object Obj)
        {
            if (Obj != null)
            {
                return Obj.ToString();
            }

            return string.Empty;
        }
    }
}
