using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public class PlanetScreenAlign : Align
    {
        SystemScreen parentScreen;
        int planetId;
        // width = 389
        public PlanetScreenAlign(SystemScreen parentScreen, int planetId, int width = 550, int height = 293, int leftMargin = 2, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.parentScreen = parentScreen;
        }

        public override Rectangle GetRect(ViewController view)
        {
            Rectangle Rect = new Rectangle(parentScreen.Rect.Right, parentScreen.Rect.Top, width, height);
            Debug.WriteLine(view.viewPortWidth);
            if (Rect.Right > view.viewPortWidth - parentScreen.Parent.align.rightMargin)
            {
                Rect.X = parentScreen.Rect.Left - parentScreen.Rect.Width;
            }
            return Rect;
        }
    }
}
