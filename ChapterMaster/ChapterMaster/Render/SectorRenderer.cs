using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    class SectorRenderer
    {
        public SectorRenderer() { }
        Texture2D Pixel;

        public void Initialize()
        {
            Pixel = new Texture2D(ChapterMaster.graphicsDevice,1,1);
            Pixel.SetData<Color>(new Color[] { Color.White });
        }
        public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            double angle = Math.Atan2(end.Y-start.Y,end.X-start.X);
            spriteBatch.Draw(Pixel, null, new Rectangle((int)start.X, (int)start.Y, (int)end.Length(), 1),
                null, null, (float) angle,
                null, color, 
                SpriteEffects.None, 0);
        }
        public void DrawStar(SpriteBatch spriteBatch, System system, int scaleX, int scaleY)
        {
            spriteBatch.Draw(ChapterMaster.SystemTextures[system.color], new Rectangle(system.x, system.y, 80 * scaleX, 80 * scaleY), Color.White);
        }
        public void Render(SpriteBatch spriteBatch, Sector sector)
        {
            
            foreach (System system in sector.Systems)
            {
                //Console.WriteLine("x " + system.x + " y " + system.y);
                DrawStar(spriteBatch, system);
            }
        }
    }
}
