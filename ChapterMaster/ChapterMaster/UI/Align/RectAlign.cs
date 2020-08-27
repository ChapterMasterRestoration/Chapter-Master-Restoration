using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public class RectAlign : Align
    {
        public bool Pinned;
        Screen screen;
        public Vector2 position;
        public RectAlign(Screen screen, Vector2 position, int width, int height, int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.screen = screen;
            this.position = position;
        }

        public override Rectangle GetRect(ViewController view)
        {
            //Pain.
            if (!Pinned)
            {
                return new Rectangle(screen.position.X + position.X, screen.position.Y + position.Y, width, height);
            }
            else
            {
                return new Rectangle(view.camX + screen.position.X + position.X, view.camY + screen.position.Y + position.Y, width, height);
            }
        }
    }
}
