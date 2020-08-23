using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class InvisibleButton : Button
    {
        public InvisibleButton(Align align, MouseHandler mouseHandler) : base(-1, "", align, mouseHandler)
        {

        }
    }
}
