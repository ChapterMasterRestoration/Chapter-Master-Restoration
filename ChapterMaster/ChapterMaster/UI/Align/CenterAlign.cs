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
            float scaleX = ((float) 1 / (float) 800) * view.viewPortWidth; // This doesn't work.
            float scaleY = ((float) 1 / (float) 600) * view.viewPortHeight;
            leftMargin = (int) (width * scaleX / 2);
            topMargin = (int) (height * scaleY / 2);
            return new Rectangle((int)(centerX - (width * scaleX / 2)),(int) (centerY - (height * scaleY / 2)), (int) (width * scaleX), (int) (height * scaleY)); // TODO Scale by window size.
        }
    }
}
