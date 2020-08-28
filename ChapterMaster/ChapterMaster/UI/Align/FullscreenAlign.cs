using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public class MapFrameAlign : Align
    {
        public MapFrameAlign(int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(0,0,leftMargin, topMargin, rightMargin, bottomMargin)
        {
        }

        public override Rectangle GetRect(ViewController view)
        {
            width = view.viewPortWidth;
            height = view.viewPortHeight;
            return new Rectangle(0, 0, view.viewPortWidth, view.viewPortHeight);
        }
    }
}
