using System;
using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Align
{
    public enum Edge
    {
        LEFT, RIGHT, TOP, BOTTOM
    }
    public class EdgeAlign : Align
    {
        Edge edge;
        public Align superAlign;
        public EdgeAlign(int width, int height, Edge edge, Align superAlign = null, int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.edge = edge;
            this.superAlign = superAlign;
        }

        public override Rectangle GetRect(ViewController view)
        {
            switch (edge)
            {
                
            }

            throw new NotImplementedException();
        }
    }
}