using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public enum Corner
    {
        TOPLEFT, TOPRIGHT, BOTTOMRIGHT, BOTTOMLEFT
    }
    public class CornerAlign : Align
    {
        Corner Corner;
        Align subAlign;
        public CornerAlign(Corner corner, int width, int height,int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width,height,leftMargin, topMargin, rightMargin, bottomMargin)
        {
            Corner = corner;
        }
        public CornerAlign(Corner corner, int width, int height, Align subAlign,int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            Corner = corner;
            this.subAlign = subAlign;
        }
        public override Rectangle GetRect(ViewController view)
        {
            
            switch(Corner)
            {
                case Corner.TOPLEFT:
                    //Console.WriteLine(Screen.align.leftMargin);
                    if(subAlign != null)
                        return new Rectangle(subAlign.leftMargin + leftMargin, subAlign.topMargin + topMargin, width, height);
                    else
                        return new Rectangle(Screen.align.leftMargin + leftMargin, Screen.align.topMargin + topMargin, width, height);
                case Corner.TOPRIGHT:

                    break;
                case Corner.BOTTOMLEFT:
                    return new Rectangle(Screen.align.leftMargin + leftMargin, view.viewPortHeight - Screen.align.bottomMargin - bottomMargin - height, width, height); // DOES NOT WORK LIKE CORNER.BOTTOMRIGHT.
                case Corner.BOTTOMRIGHT:
                    return new Rectangle(view.viewPortWidth - Screen.align.rightMargin - width, view.viewPortHeight - Screen.align.bottomMargin - height,width,height);
                //break; idk if I wanna do it differently
            }
            return new Rectangle(0, 0, width, height);
        }
    }
}
