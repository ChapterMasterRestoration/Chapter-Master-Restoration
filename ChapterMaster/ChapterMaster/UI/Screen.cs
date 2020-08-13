using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    class Screen
    {
        public List<Screen> Screens = new List<Screen>();
        public List<Button> Buttons = new List<Button>();
        public void Render(SpriteBatch spriteBatch, ViewController view)
        {
            foreach(Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
            foreach (Screen screen in Screens)
            {
                screen.Render(spriteBatch, view);
            }
        }

        public void Update(ViewController view)
        {
            foreach(Button button in Buttons)
            {
                button.Check(view);
            }
            foreach (Screen screen in Screens)
            {
                screen.Update(view);
            }
        }
    }
}
