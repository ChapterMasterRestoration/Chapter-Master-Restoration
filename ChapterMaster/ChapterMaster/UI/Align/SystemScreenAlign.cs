using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public class SystemScreenAlign : Align
    {
        int systemId;
        Align parentAlign;
        public bool pinned;
        public SystemScreenAlign(Align parentAlign, int systemId, int width = 320, int height = 294, int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.systemId = systemId;
            this.parentAlign = parentAlign;
        }

        public override Rectangle GetRect(ViewController view)
        {
            Rectangle rect;
            if (!pinned)
            {
                rect = view.TransformedOriginRect(GameState.sector.Systems[systemId].x,
                                           GameState.sector.Systems[systemId].y, width, height, false);
            } else
            {
                rect = new Rectangle(GameState.sector.Systems[systemId].x, GameState.sector.Systems[systemId].y, width, height);
            }
            if (rect.Top < parentAlign.topMargin)
            {
                rect.Y = parentAlign.topMargin;
            }
            if (rect.Bottom > parentAlign.height - parentAlign.bottomMargin)
            {
                rect.Y = parentAlign.height - parentAlign.bottomMargin - rect.Height;
            }
            if (rect.Right > parentAlign.width - parentAlign.rightMargin)
            {
                rect.X = parentAlign.width - rightMargin - rect.Width;
            }
            if (rect.Left < parentAlign.leftMargin)
            {
                rect.Y = parentAlign.leftMargin;
            }
            return rect;
        }
    }
}
