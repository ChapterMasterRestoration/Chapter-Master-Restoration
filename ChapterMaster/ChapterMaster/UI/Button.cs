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
        public Vector2 position;
        public Vector2 size;
        
        public Button(int buttonTextureId, string text, Vector2 position, Vector2 size, MouseHandler mouseHandler) : base(mouseHandler)
        {
            this.buttonTextureId = buttonTextureId;
            // TODO: Fix this
            this.position = position;
            this.size = size;
            x = (int) position.X;
            y = (int) position.Y;
            width = (int) size.X;
            height = (int) size.Y;
            this.text = text;
        }
        public void Render(SpriteBatch spriteBatch, ViewController view)
        {
            spriteBatch.Draw(ChapterMaster.ButtonTextures[buttonTextureId], position, Color.White);
            spriteBatch.DrawString(ChapterMaster.caslon_antique_regular, text, position, Color.White);
        }
    }
}
