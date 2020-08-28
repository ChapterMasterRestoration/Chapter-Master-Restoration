using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Align
{
    public abstract class Align
    {
        public Screen Screen;
        public int leftMargin;
        public int topMargin;
        public int rightMargin;
        public int bottomMargin;
        public int width;
        public int height;
        public bool Pinned;
        public Align(int width, int height,int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            this.leftMargin = leftMargin;
            this.topMargin = topMargin;
            this.rightMargin = rightMargin;
            this.bottomMargin = bottomMargin;
            this.width = width;
            this.height = height;
        }

        public abstract Rectangle GetRect(ViewController view);
    }
}
