using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI.Align
{
    public class PlanetAlign : Align
    {
        public int planetNo;
        public Vector2 starPos;
        public Vector2 planetPos;
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
            planetPos = new Vector2(leftMargin + width + starPos.X + planetNo * rightMargin, starPos.Y - (height / 2) + topMargin);
            return new Rectangle((int)planetPos.X, (int)planetPos.Y, width, height);
        }
    }
}
