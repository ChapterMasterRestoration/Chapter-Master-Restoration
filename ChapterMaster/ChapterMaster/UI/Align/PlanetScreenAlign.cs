using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
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
            Rectangle Rect = parentScreen.Rect;
            return new Rectangle(Rect.Right,Rect.Top, width, height);
        }
    }
}
