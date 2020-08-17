using ChapterMaster.Render;
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
        public PlanetScreen(int screenId, string backgroundTexture, int systemId, Align align) : base(screenId, backgroundTexture, align)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.systemId = systemId;
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            // TODO: replace with align
            Rect = view.TransformedOriginRect(ChapterMaster.sector.Systems[systemId].x, 
                                              ChapterMaster.sector.Systems[systemId].y, 300, 100, false);
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            // TODO: replace with align component
            Point position = Rect.Location + new Point(0, 20);
            // TODO: better way to pass the system's color
            RenderHelper.DrawStar(spriteBatch,new Vector2(position.X,position.Y),ChapterMaster.sector.Systems[systemId].color);
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
