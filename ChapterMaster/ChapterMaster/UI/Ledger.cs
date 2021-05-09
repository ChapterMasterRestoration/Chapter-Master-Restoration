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
        private bool enabled = true;
        public Ledger(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            Button closeLedger = new Button("creation_arrow_right", "", new EdgeAlign(Edge.TOPLEFTOFRIGHT,20,20, align, leftMargin:6, topMargin:-22), ToggleLedger);
            AddButton(closeLedger);
        }

        private void ToggleLedger(MouseState mouseState, object sender)
        {
            Debug.WriteLine("hiding ledger");
            if (enabled)
            {
                AddAnimation(new Slide(SlideDirection.RIGHT, 0, 90, 0, 0, onReachEndOfAnimation, Buttons[0], this));
                enabled = false;

            }
            else
            {
                AddAnimation(new Slide(SlideDirection.LEFT, -90, 0, 0, 0, onReachEndOfAnimation, Buttons[0], this));
                enabled = true;

            }
        }

        private void onReachEndOfAnimation(object animatedObject, object sender)
        {
            if (sender != null && animatedObject != null)
            {
                if (((Ledger) sender).GetEnabled())
                {
                    ((Button) animatedObject).buttonTextureId = "creation_arrow_right";
                }
                else
                {
                    ((Button) animatedObject).buttonTextureId = "creation_arrow_left";
                }
            }
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