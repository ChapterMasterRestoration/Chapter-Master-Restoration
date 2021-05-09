using System.Diagnostics;
using ChapterMaster.UI.Align;
using ChapterMaster.UI.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChapterMaster.UI
{
    public class Ledger : Screen
    {
        private bool enabled = false;
        public Ledger(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            Button closeLedger = new Button("creation_arrow_right", "", new EdgeAlign(Edge.TOPLEFTOFRIGHT,20,20, align, leftMargin:6, topMargin:-22), HideLedger);
            AddButton(closeLedger);
        }

        private void HideLedger(MouseState mouseState, object sender)
        {
            AddAnimation(new Slide(SlideDirection.RIGHT,0,280,0,0));
        }

        public bool GetEnabled()
        {
            return enabled;
        }

        public void Toggle()
        {
            enabled = !enabled;
        }
        
    }
}