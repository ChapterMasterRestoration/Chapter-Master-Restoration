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
    class InteractiveElement
    {
        public int x, y;
        public int width, height;
        MouseHandler handler;
        bool wasClicked = false;
        public InteractiveElement(MouseHandler mouseHandler)
        {
            handler = mouseHandler;
        }

        public void Check(ViewController view)
        {
            int mouseX = view.GetMouse().X;
            int mouseY = view.GetMouse().Y;
            int ulCornerX = x;
            int ulCornerY = y;
            int brCornerX = x + width;
            int brCornerY = y + height;
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
