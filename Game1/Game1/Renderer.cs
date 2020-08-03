using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    class Renderer
    {
        public Renderer() { }
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
    }
}
