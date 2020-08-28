using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public class RectAlign : Align
    {
        public Vector2 position;
        public RectAlign(Screen screen, Vector2 position, int width, int height, int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.Screen = screen;
            this.position = position;
        }

        public override Rectangle GetRect(ViewController view)
        {
            //Pain.
            if (!Pinned)
            {
                return new Rectangle((int)(Screen.Rect.Location.X + position.X), (int)(Screen.Rect.Location.Y + position.Y), width, height);
            }
            else
            {
                return new Rectangle((int)(view.camX + Screen.Rect.Location.X + position.X), (int)(view.camY + Screen.Rect.Location.Y + position.Y), width, height);
            }
        }
    }
}
