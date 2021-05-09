using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public delegate void MouseHandler(MouseState mouseState, object sender);
    public class InteractiveElement
    {
        public Vector2 position;
        public int width, height;
        protected MouseHandler handler;
        protected bool wasClicked = false;
        protected bool inFocus = false;
        protected List<Animation.Animation> Animations = new List<Animation.Animation>();
        public InteractiveElement(MouseHandler mouseHandler)
        {
            handler = mouseHandler;
        }

        public virtual void Check(ViewController view, Align.Align align)
        {
            // int mouseX = view.GetMouse().X;
            // int mouseY = view.GetMouse().Y;
            // int ulCornerX = (int)position.X;
            // int ulCornerY = (int)position.Y;
            // int brCornerX = ulCornerX + align.width;
            // int brCornerY = ulCornerY + align.height;
            Rectangle Rect = new Rectangle((int)position.X,(int)position.Y, align.width, align.height);
            for (int i = 0; i < Animations.Count; i++)
            {
                //Rect = Animations[i].Apply(Rect, view.animationDelta);
            }

            //if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
            if (Rect.Contains(view.GetMouse().Position)) 
            {
                if (view.GetMouse().LeftButton == ButtonState.Released) wasClicked = false;
                if (view.GetMouse().LeftButton == ButtonState.Pressed && !wasClicked)
                {
                    handler(view.GetMouse(), this);
                    wasClicked = true;
                }
            }
        }
        public void AddAnimation(Animation.Animation animation)
        {
            Animations.Add(animation);
        }
    }
}
