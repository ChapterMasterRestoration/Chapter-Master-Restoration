using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Render
{
    class RenderHelper
    {
        public static PrimitiveBuddy.Primitive PrimitiveBuddy;
        public static void Initialize(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            PrimitiveBuddy = new PrimitiveBuddy.Primitive(graphicsDevice, spriteBatch);
        }
        public static void DrawStar(SpriteBatch spriteBatch, Rectangle rect, int color, Align align = null, ViewController view = null)
        {
            spriteBatch.Draw(GameState.SystemTextures[color], rect, Color.White);
        }
        public static void DrawStar(SpriteBatch spriteBatch,Vector2 position, int color,Align align = null, ViewController view = null)
        {
            if (align == null)
            {
                spriteBatch.Draw(GameState.SystemTextures[color], position, Color.White);
            } else if(align != null && view != null)
            {
                spriteBatch.Draw(GameState.SystemTextures[color], align.GetRect(view), Color.White);
            }
        }
        public static void DrawPlanet(SpriteBatch spriteBatch, Vector2 position, int texture, Align align = null, ViewController view = null)
        {
            if (align == null)
            {
                spriteBatch.Draw(GameState.PlanetTextures[texture], position, Color.White);
            }
            else if (align != null && view != null)
            {
                spriteBatch.Draw(GameState.PlanetTextures[texture], align.GetRect(view), Color.White);
            }
        }
    }
}
