using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    class InteractiveElement
    {
        public int x, y;
        public int width, height;
        public delegate void ButtonPress();
        public void Check(MouseState mouseState)
        {
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            int ulCornerX = x;
            int ulCornerY = y;
            int brCornerX = x + width;
            int brCornerY = y + height;
            if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Debug.WriteLine("Hi");
                }
            }
        }
    }
}
