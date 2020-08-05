using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    class ViewController
    {
        public int camX = 0;
        public int camY = 0;
        public int scaleX = 1;
        public int scaleY = 1;
        float zoom = 1;

        public void UpdateKeyboard()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Up)) {
                camY -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camY += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camX += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camX -= 1;
            }
        }
        public void UpdateMouse()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            { 
                Console.WriteLine("x " + Mouse.GetState().X + " y " + Mouse.GetState().Y);
            }
        }
        public void Update() { 
        }

    }
}
