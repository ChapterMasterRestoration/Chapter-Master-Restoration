using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace ChapterMaster.UI
{
    public class Screen
    {
        public int screenId;
        protected string backgroundTexture;
        public Rectangle Rect;
        public List<Screen> Screens = new List<Screen>();
        public List<Button> Buttons = new List<Button>();
        public Screen(int screenId, string backgroundTexture)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
        }
        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            foreach (Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
            foreach (Screen screen in Screens)
            {
                screen.Render(spriteBatch, view);
            }
        }

        public virtual void Update(ViewController view)
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
