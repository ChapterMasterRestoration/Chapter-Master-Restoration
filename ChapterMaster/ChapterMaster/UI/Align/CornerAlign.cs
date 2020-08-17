using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public enum Corner
    {
        TOPLEFT, TOPRIGHT, BOTTOMRIGHT, BOTTOMLEFT
    }
    public class CornerAlign : Align
    {
        Corner Corner;

        public CornerAlign(Corner corner, int width, int height,int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width,height,leftMargin, topMargin, rightMargin, bottomMargin)
        {
            Corner = corner;
        }

        public override Rectangle GetRect(ViewController view)
        {

            switch(Corner)
            {
                case Corner.TOPLEFT:

                    break;
                case Corner.TOPRIGHT:

                    break;
                case Corner.BOTTOMLEFT:

                    break;
                case Corner.BOTTOMRIGHT:
                    return new Rectangle(view.viewPortWidth - Screen.align.rightMargin - width, view.viewPortHeight - Screen.align.bottomMargin - height,width,height);
                    break;
                default:
                    throw new NotImplementedException("Not a real corner.");
            }
            return new Rectangle(0, 0, 100, 100);
        }
    }
}
