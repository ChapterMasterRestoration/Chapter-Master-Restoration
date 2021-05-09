using System;
using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Align
{
    public enum Edge
    {
        TOPLEFTOFRIGHT, RIGHT, TOP, BOTTOM // I do not care anymore. ;-;
    }
    public class EdgeAlign : Align
    {
        Edge edge;
        public Align superAlign;
        public EdgeAlign(Edge edge,int width, int height, Align superAlign, int leftMargin = 0, int topMargin = 0, int rightMargin = 0, int bottomMargin = 0) : base(width, height, leftMargin, topMargin, rightMargin, bottomMargin)
        {
            this.edge = edge;
            this.superAlign = superAlign;
        }

        public override Rectangle GetRect(ViewController view)
        {
            switch (edge)
            {
                case Edge.TOPLEFTOFRIGHT:
                    return new Rectangle(view.viewPortWidth - superAlign.width - superAlign.leftMargin + leftMargin, view.viewPortHeight - (int) (superAlign.topMargin + superAlign.height + height) + topMargin, width, height);
            }

            throw new NotImplementedException();
        }
    }
}