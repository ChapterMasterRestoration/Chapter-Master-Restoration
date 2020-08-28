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
        MouseHandler handler;
        bool wasClicked = false;
        public InteractiveElement(MouseHandler mouseHandler)
        {
            handler = mouseHandler;
        }

        public void Check(ViewController view, Align.Align align)
        {
            int mouseX = view.GetMouse().X;
            int mouseY = view.GetMouse().Y;
            int ulCornerX = (int)position.X;
            int ulCornerY = (int)position.Y;
            int brCornerX = ulCornerX + align.width;
            int brCornerY = ulCornerY + align.height;
            if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
            {
                if (view.GetMouse().LeftButton == ButtonState.Released) wasClicked = false;
                if (view.GetMouse().LeftButton == ButtonState.Pressed && !wasClicked)
                {
                    handler(view.GetMouse(), this);
                    wasClicked = true;
                }
            }
        }
    }
}
