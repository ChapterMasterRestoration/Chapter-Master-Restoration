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
        public int buttonTextureId;
        public string text = "";

        public Align.Align align;
        protected Button(int buttonTextureId, string text, MouseHandler mouseHandler) : base(mouseHandler)
        {
            this.buttonTextureId = buttonTextureId;
            this.text = text;
        }
        public Button(int buttonTextureId, string text, Align.Align align, MouseHandler mouseHandler) : this(buttonTextureId, text, mouseHandler)
        {
            // TODO: Fix this
            this.align = align;
        }
        public virtual void Render(SpriteBatch spriteBatch, ViewController view)
        {
            if (buttonTextureId >= 0)
            {
                position = new Vector2(align.GetRect(view).X, align.GetRect(view).Y); // TODO: fix: don't even expose position
                spriteBatch.Draw(GameState.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
                spriteBatch.DrawString(GameState.Caslon_Antique_Regular, text, position, Color.White);
            }
        }
    }
}
