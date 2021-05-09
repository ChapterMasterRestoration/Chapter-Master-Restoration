using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class Button : InteractiveElement
    {
        public string buttonTextureId = "";
        public string text = "";

        public Align.Align align;
        protected Button(string buttonTextureId, string text, MouseHandler mouseHandler) : base(mouseHandler)
        {
            this.buttonTextureId = buttonTextureId;
            this.text = text;
        }
        public Button(string buttonTextureId, string text, Align.Align align, MouseHandler mouseHandler) : this(buttonTextureId, text, mouseHandler)
        {
            // TODO Fix this
            this.align = align;
        }
        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            Rectangle Rect = align.GetRect(view);
            //position = new Vector2(align.GetRect(view).X, align.GetRect(view).Y); // TODO fix: don't even expose position
            for (int i = 0; i < Animations.Count; i++)
            {
                Rect = Animations[i].Apply(Rect, view.animationDelta);
            }
            position = Rect.Location.ToVector2(); // ???
            if (buttonTextureId.Length > 0)
            {
                spriteBatch.Draw(Assets.ButtonTextures[buttonTextureId], Rect, Color.White);
            }
            spriteBatch.DrawString(Assets.Caslon_Antique_Regular, text, position, Color.White);
        }
    }
}
