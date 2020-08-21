using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class PlanetAlign : Align
    {
        public int planetNo;
        public Vector2 starPos;
        /// <summary>
        /// Not sure how to implement this for now.
        /// </summary>
        /// <param name="planetNo"></param>
        /// <param name="starPos"></param>
        /// <param name="leftMargin">margin from star to first planet</param>
        /// <param name="rightMargin">margin between planets</param>
        /// <param name="topMargin"></param>
        /// <param name="bottomMargin"></param>
        public PlanetAlign(int planetNo, Vector2 starPos, int leftMargin, int rightMargin, int topMargin = 0,int bottomMargin = 0) : base(32, 32, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.planetNo = planetNo;
            this.starPos = starPos;
        }

        public override Rectangle GetRect(ViewController view)
        {
            return new Rectangle((int) (leftMargin + starPos.X + planetNo * rightMargin), (int) starPos.Y + topMargin, width, height);
        }
    }
}
