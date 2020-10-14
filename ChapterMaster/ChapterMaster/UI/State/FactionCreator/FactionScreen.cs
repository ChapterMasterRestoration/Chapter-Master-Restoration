using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ChapterMaster.UI.State.FactionCreator
{
    public class FactionScreen : Screen
    {
        public PrimitiveBuddy.Primitive primitive;

        public FactionScreen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {

        }


        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            base.Render(spriteBatch, view);
        }

        public override void Update(ViewController view)
        {
            base.Update(view);
        }
    }
}
