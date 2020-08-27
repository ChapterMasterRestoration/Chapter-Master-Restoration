﻿using Microsoft.Xna.Framework;
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

        public Align align;
        protected Button(int buttonTextureId, string text, MouseHandler mouseHandler) : base(mouseHandler)
        {
            this.buttonTextureId = buttonTextureId;
            this.text = text;
        }
        public Button(int buttonTextureId, string text, Align align, MouseHandler mouseHandler) : this(buttonTextureId, text, mouseHandler)
        {
            // TODO: Fix this
            this.align = align;
        }
        public void Render(SpriteBatch spriteBatch, ViewController view)
        {
            if (buttonTextureId >= 0)
            {
                position = new Vector2(align.GetRect(view).X, align.GetRect(view).Y); // TODO: fix: don't even expose position
                spriteBatch.Draw(ChapterMaster.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
                spriteBatch.DrawString(ChapterMaster.Caslon_Antique_Regular, text, position, Color.White);
            }
        }
    }
}
