using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class PinButton : Button
    {
        Screen screen;
        public PinButton(MouseHandler mouseHandler,Screen screen, int buttonTextureId = 4) : base(buttonTextureId,"", mouseHandler)
        {
            this.buttonTextureId = buttonTextureId;
            // TODO: Fix this
            this.align = new RectAlign(screen, position, width = 32, height = 32);
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            if (buttonTextureId >= 0)
            {
                ((RectAlign)align).position = position;
                spriteBatch.Draw(ChapterMaster.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
            }
        }
    }
}
