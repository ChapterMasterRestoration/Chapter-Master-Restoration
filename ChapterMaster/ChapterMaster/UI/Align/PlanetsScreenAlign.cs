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
        int systemId;
        public PlanetScreenAlign(int systemId,int width = 320, int height = 294,int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.systemId = systemId;
        }

        public override Rectangle GetRect(ViewController view)
        {
            return view.TransformedOriginRect(ChapterMaster.sector.Systems[systemId].x,
                                              ChapterMaster.sector.Systems[systemId].y, width, height, false);
        }
    }
}
