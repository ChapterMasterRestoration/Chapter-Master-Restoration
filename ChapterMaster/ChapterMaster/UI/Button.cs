using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    class Button : InteractiveElement
    {
        public int buttonTextureId;
        public string text = "";
        public Vector2 position;
        public Vector2 size;
        
        public Button(int _buttonTextureId, string _text, Vector2 _position, Vector2 _size, MouseHandler mouseHandler) : base(mouseHandler)
        {
            buttonTextureId = _buttonTextureId;
            // TODO: Fix this
            position = _position;
            size = _size;
            x = (int) position.X;
            y = (int) position.Y;
            width = (int) size.X;
            height = (int) size.Y;
            text = _text;
        }
        public void Render(SpriteBatch spriteBatch, ViewController view)
        {
            spriteBatch.Draw(ChapterMaster.ButtonTextures[buttonTextureId], position, Color.White);
            spriteBatch.DrawString(ChapterMaster.caslon_antique_regular, text, position, Color.White);
        }
    }
}
