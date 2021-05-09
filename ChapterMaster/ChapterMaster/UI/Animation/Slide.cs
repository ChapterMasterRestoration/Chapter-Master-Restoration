using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ChapterMaster.UI.Animation
{
    public enum SlideDirection
    {
        LEFT, RIGHT, TOP, BOTTOM
    }
    public class Slide : Animation
    {
        private SlideDirection direction;
        public Slide(SlideDirection slideDirection, int leftBound, int rightBound, int topBound, int bottomBound, OnFinish onFinish = null, object animatedObject = null, object owner = null) : base(leftBound, rightBound, topBound, bottomBound, onFinish, animatedObject, owner)
        {
            direction = slideDirection;
        }

        public static Vector2 GetDirection(SlideDirection direction)
        {
            switch (direction)
            {
                case SlideDirection.LEFT:
                    return new Vector2(-1.0f, 0f);
                case SlideDirection.RIGHT:
                    return new Vector2(1.0f, 0f);
                case SlideDirection.TOP:
                    return new Vector2(0f, -1.0f);
                case SlideDirection.BOTTOM:
                    return new Vector2(0f, 1.0f);
                default:
                    throw new Exception();
            }
        }

        public bool reachedTheEnd = false;
        public Vector2 Bound(Vector2 d)
        {
            Vector2 newBound = d;
            if (activated && !reachedTheEnd)
            {
                if (d.X < leftBound)
                {
                    activated = false;
                    reachedTheEnd = true;
                    newBound = new Vector2(leftBound, d.Y);
                }
                else if (d.X > rightBound)
                {
                    activated = false;
                    reachedTheEnd = true;
                    Debug.WriteLine("deactivated at " + d.X + " bound= " + rightBound + " " + reachedTheEnd);
                    newBound = new Vector2(rightBound, d.Y);
                }

                if (d.Y < topBound)
                {
                    activated = false;
                    reachedTheEnd = true;
                    newBound = new Vector2(d.X, topBound);
                }
                else if (d.Y > bottomBound)
                {
                    activated = false;
                    reachedTheEnd = true;
                    newBound = new Vector2(d.X, bottomBound);
                }
            }
            if (reachedTheEnd)
            {
                Debug.WriteLine("reached the end of animation");
                onFinish(animatedObject, owner);
                reachedTheEnd = false;
            }
            return newBound;
        }

        private Vector2 d = new Vector2(0, 0); // TODO: Implement starting position in constructor;
        public override Rectangle Apply(Rectangle rectangle, float delta = 10f)
        {
            Vector2 p = new Vector2(rectangle.Left, rectangle.Top);
            if (activated)
            {
                d = Bound(d + delta * Slide.GetDirection(direction));
                //Debug.WriteLine("animation pos: pX: " + p.X + " pY: " + p.Y + " dX: " + d.X + " dY: " + d.Y + " dirY: " + Slide.GetDirection(direction).X + " delta: " + delta);
            }
            p = p + d;
            return new Rectangle((int)p.X,(int)p.Y,rectangle.Width, rectangle.Height);
        }
    }
}