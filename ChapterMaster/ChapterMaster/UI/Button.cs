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
            // TO DO: Fix this
            this.align = align;
        }
        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            position = new Vector2(align.GetRect(view).X, align.GetRect(view).Y); // TO DO: fix: don't even expose position
            if (buttonTextureId.Length > 0)
            {
                spriteBatch.Draw(Assets.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
            }
            spriteBatch.DrawString(Assets.Caslon_Antique_Regular, text, position, Color.White);
        }
    }
}
