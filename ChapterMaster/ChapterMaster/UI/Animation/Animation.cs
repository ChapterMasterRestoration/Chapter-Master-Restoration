using System;
using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Animation
{
    public delegate void OnFinish(object animatedObject, object owner);
    public class Animation
    {
        public int leftBound;
        public int rightBound;
        public int topBound;
        public int bottomBound;
        public bool activated = true;
        protected OnFinish onFinish;
        protected object animatedObject;
        protected object owner;
        public Animation(int leftBound, int rightBound, int topBound, int bottomBound, OnFinish onFinish = null, object animatedObject = null, object owner = null)
        {
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            this.topBound = topBound;
            this.bottomBound = bottomBound;
            this.onFinish = onFinish;
            this.animatedObject = animatedObject;
            this.owner = owner;
        }

        public virtual Rectangle Apply(Rectangle rectangle, float delta = 10f)
        {
            return rectangle;
        }
    }
}