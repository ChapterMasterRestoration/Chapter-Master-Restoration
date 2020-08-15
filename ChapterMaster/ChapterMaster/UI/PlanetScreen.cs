using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class PlanetScreen : Screen
    {
        public int systemId;
        public PlanetScreen(int screenId, string backgroundTexture, int systemId) : base(screenId, backgroundTexture)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.systemId = systemId;
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            int xPos = (int)((ChapterMaster.sector.Systems[systemId].x - view.camX) * view.zoom + ChapterMaster.GetWidth() / 2);
            int yPos = (int)((ChapterMaster.sector.Systems[systemId].y - view.camY) * view.zoom + ChapterMaster.GetHeight() / 2);
            // TODO: align and scale to viewport.
            Rect = new Rectangle(xPos, yPos, 300, 100);
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            foreach (Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
            foreach (Screen screen in Screens)
            {
                screen.Render(spriteBatch, view);
            }
        }
    }
}
