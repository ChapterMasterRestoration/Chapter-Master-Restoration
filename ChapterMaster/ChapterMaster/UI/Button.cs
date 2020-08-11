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
        public Button(int _buttonTextureId, string _text, Vector2 _position)
        {
            buttonTextureId = _buttonTextureId;
            position = _position;
            x = (int) position.X;
            y = (int) position.Y;
            width = 144;
            height = 43;
            text = _text;
        }
        public void Render(SpriteBatch spriteBatch, ViewController view)
        {
            spriteBatch.Draw(ChapterMaster.ButtonTextures[buttonTextureId], position, Color.White);
            spriteBatch.DrawString(ChapterMaster.caslon_antique_regular, text, position, Color.White);
        }
    }
}
