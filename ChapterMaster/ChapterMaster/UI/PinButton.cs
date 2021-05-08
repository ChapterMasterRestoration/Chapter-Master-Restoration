using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class PinButton : Button
    {

        /// <summary>
        /// A part of the bourgeoisie is desirous of redressing social
        /// grievances, in order to secure the continued existence of
        /// bourgeois society.
        /// </summary>
        Screen screen;
        public PinButton(MouseHandler mouseHandler,Screen screen, string buttonTextureId = "pinbutton") : base(buttonTextureId,"", mouseHandler)
        {
            this.buttonTextureId = buttonTextureId;
            // TODO: Fix this
            this.align = new RectAlign(screen, position, width = 32, height = 32);
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            if (buttonTextureId.Length > 0)
            {
                ((RectAlign)align).position = position;
                spriteBatch.Draw(Assets.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
            }
        }
        public override void Check(ViewController view, Align.Align align)
        {
            if (align.GetRect(view).Contains(view.GetMouse().Position)) {
                if (view.GetMouse().LeftButton == ButtonState.Released) wasClicked = false;
                if (view.GetMouse().LeftButton == ButtonState.Pressed && !wasClicked)
                {
                    handler(view.GetMouse(), this);
                    wasClicked = true;
                }
            }
        }
    }
}
