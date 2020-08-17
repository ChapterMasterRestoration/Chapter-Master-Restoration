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
        public PlanetScreenAlign(int width, int height,int leftMargin, int topMargin, int rightMargin, int bottomMargin) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
        }

        public override Rectangle GetRect(ViewController view)
        {
            throw new NotImplementedException();
        }
    }
}
