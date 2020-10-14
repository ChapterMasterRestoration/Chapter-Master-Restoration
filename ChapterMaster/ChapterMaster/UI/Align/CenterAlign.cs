using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Align
{
    public class CenterAlign : Align
    {
        public CenterAlign(int width, int height, int leftMargin, int topMargin, int rightMargin, int bottomMargin) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {

        }

        public override Rectangle GetRect(ViewController view)
        {
            int centerX = view.viewPortWidth / 2;
            int centerY = view.viewPortHeight / 2;
            float scaleX = ((float) width / (float) 800) * view.viewPortWidth; // This doesn't work.
            float scaleY = ((float) height / (float) 600) * view.viewPortHeight;

            return new Rectangle(centerX - (width / 2), centerY - (height / 2), (int) (width * scaleX), (int) (height * scaleY)); // TO DO: Scale by window size.
        }
    }
}
