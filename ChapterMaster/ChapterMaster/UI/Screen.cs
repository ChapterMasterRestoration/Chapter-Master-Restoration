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
        public List<Animation.Animation> Animations = new List<Animation.Animation>();
        public Align.Align align;
        public bool WasModified;
        public bool DoesOcclusion;

        public Screen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.align = align;
            this.align.Screen = this;
            this.DoesOcclusion = DoesOcclusion;
        }

        public virtual void Update(ViewController view)
        {
            foreach (Button button in Buttons)
            {
                button.Check(view, button.align);
            }
            for (int n = 0; n < Screens.Count; n++)
            {
                if (WasModified)
                {
                    WasModified = false;
                    break;
                }
                Screens[n].Update(view);
            }
            if (DoesOcclusion)
            {
                // TODO: Implement more advanced occlusion.
                if (Rect.Contains(view.GetMouse().Position))
                {
                    view.IsOccluded = true;
                }
            }
        }

        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            Rect = align.GetRect(view);
            for (int i = 0; i < Animations.Count; i++)
            {
                Rect = Animations[i].Apply(Rect, view.animationDelta);
            }
            spriteBatch.Draw(Assets.UITextures[backgroundTexture], Rect, Color.White);
            foreach (Button button in Buttons)
            {
                // TODO: implement button animations
                button.Render(spriteBatch, view);
            }
            // The proletariat will rise.
            for (int n = 0; n < Screens.Count; n++)
            {
                if(WasModified)
                {
                    //wasModified = false; // idk
                    //break;
                }
                for (int i = 0; i < Animations.Count; i++)
                {
                    Screens[n].AddAnimation(Animations[i]);
                }
                Screens[n].Render(spriteBatch, view);
            }
        }

        // Marx will be proud.
        public virtual void ExitScreen(MouseState mouseState, object sender)
        {

        }
        public void AddChildScreen(Screen screen)
        {
            Screen screen1 = screen;
            screen1.Parent = this;
            if(Parent != null) Parent.WasModified = true;
            WasModified = true;
            Screens.Add(screen1);
        }
        public void AddButton(Button button)
        {
            Button button1 = button;
            button1.align.Screen = this;
            Buttons.Add(button1);
        }

        public void AddAnimation(Animation.Animation animation)
        {
            Animations.Add(animation);
        }
    }
}
