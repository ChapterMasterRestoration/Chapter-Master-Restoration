using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Animation
{
    public class Animation
    {
        public int leftBound;
        public int rightBound;
        public int topBound;
        public int bottomBound;
        public bool activated = true;
        public Animation(int leftBound, int rightBound, int topBound, int bottomBound)
        {
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            this.topBound = topBound;
            this.bottomBound = bottomBound;
        }

        public virtual Rectangle Apply(Rectangle rectangle, float delta = 10f)
        {
            return rectangle;
        }
    }
}