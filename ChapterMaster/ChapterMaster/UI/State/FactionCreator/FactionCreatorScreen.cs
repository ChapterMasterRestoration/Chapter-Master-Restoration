using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChapterMaster.UI.State
{
    public class FactionCreatorScreen : Screen
    {
        public PrimitiveBuddy.Primitive primitive;
        CenterAlign factionAlign;

        public FactionCreatorScreen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            this.factionAlign = new CenterAlign(400,500, 170, 80, 0, 0);
        }


        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            base.Render(spriteBatch, view);
            spriteBatch.Draw(Assets.UITextures["faction_creator"], factionAlign.GetRect(view), Color.White);
        }

        public override void Update(ViewController view)
        {
            base.Update(view);
        }
    }
}
