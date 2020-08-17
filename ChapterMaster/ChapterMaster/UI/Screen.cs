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
        public Screen Parent;
        public List<Screen> Screens = new List<Screen>();
        public List<Button> Buttons = new List<Button>();
        public Align align;

        public Screen(int screenId, string backgroundTexture, Align align)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.align = align;
            this.align.Screen = this;
        }
        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], align.GetRect(view), Color.White);
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
                button.Check(view,button.align);
            }
            foreach (Screen screen in Screens)
            {
                screen.Update(view);
            }
        }
        public void AddChildScreen(Screen screen)
        {
            Screen screen1 = screen;
            screen1.Parent = this;
            Screens.Add(screen1);
        }
        public void AddButton(Button button)
        {
            Button button1 = button;
            button1.align.Screen = this;
            Buttons.Add(button1);
        }
    }
}
