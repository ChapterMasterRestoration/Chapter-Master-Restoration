using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public Align.Align align;
        public bool wasModified;

        public Screen(int screenId, string backgroundTexture, Align.Align align)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.align = align;
            this.align.Screen = this;
        }
        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            Rect = align.GetRect(view);
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            foreach (Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
            for (int n = 0; n < Screens.Count; n++)
            {
                if(wasModified)
                {
                    //wasModified = false; // idk
                    //break;
                }
                Screens[n].Render(spriteBatch, view);
            }
        }

        public virtual void Update(ViewController view)
        {
            foreach(Button button in Buttons)
            {
                button.Check(view,button.align);
            }
            for (int n = 0; n < Screens.Count; n++)
            {
                if (wasModified)
                {
                    wasModified = false;
                    break;
                }
                Screens[n].Update(view);
            }
        }
        public virtual void ExitScreen(MouseState mouseState, object sender)
        {

        }
        public void AddChildScreen(Screen screen)
        {
            Screen screen1 = screen;
            screen1.Parent = this;
            if(Parent != null) Parent.wasModified = true;
            wasModified = true;
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
