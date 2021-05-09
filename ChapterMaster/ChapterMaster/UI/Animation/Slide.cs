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
        public Slide(SlideDirection slideDirection, int leftBound, int rightBound, int topBound, int bottomBound) : base(leftBound, rightBound, topBound, bottomBound)
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

        public Vector2 Bound(Vector2 d)
        {
            if (d.X < leftBound)
            {
                activated = false;
                return new Vector2(leftBound, d.Y);
            }
            else if (d.X > rightBound)
            {
                activated = false; Debug.WriteLine("deactivated at " + d.X + " bound= " + rightBound);
                return new Vector2(rightBound, d.Y);
            }

            if (d.Y < topBound)
            {
                activated = false;
                return new Vector2(d.X, topBound);
            }
            else if (d.Y > bottomBound)
            {
                activated = false;
                return new Vector2(d.X, bottomBound);
            }
            return d;
        }

        private Vector2 d = new Vector2(0, 0); // TODO: Implement starting position in constructor;
        public override Rectangle Apply(Rectangle rectangle, float delta = 10f)
        {
            Vector2 p = new Vector2(rectangle.Left, rectangle.Top);
            if (activated)
            {
                d = Bound(d + delta * Slide.GetDirection(direction));
                p = p + d;
                Debug.WriteLine("animation pos: pX: " + p.X + " pY: " + p.Y + " dX: " + d.X + " dY: " + d.Y + " dirY: " + Slide.GetDirection(direction).X + " delta: " + delta);
            }

            return new Rectangle((int)p.X,(int)p.Y,rectangle.Width, rectangle.Height);
        }
    }
}