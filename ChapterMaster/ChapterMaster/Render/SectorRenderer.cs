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
        public void DrawLine(SpriteBatch spriteBatch, Vector2 _start, Vector2 _end, Color color)
        {
            Vector2 start = new Vector2(Math.Min(_start.X, _end.X), Math.Min(_start.Y, _end.Y));
            Vector2 end = new Vector2(Math.Max(_start.X, _end.X), Math.Max(_start.Y, _end.Y));
            double angle = Math.Atan2(end.Y-start.Y,end.X-start.X);
            spriteBatch.Draw(Pixel, null, new Rectangle((int)start.X, (int)start.Y, (int)end.Length(), 1),
                null, null, (float) angle,
                null, color, 
                SpriteEffects.None, 0);
        }
        public void DrawStar(SpriteBatch spriteBatch, System system, ViewController view, Color color)
        {
            spriteBatch.Draw(ChapterMaster.SystemTextures[system.color], 
                new Rectangle(system.x, system.y, 80 * view.scaleX, 80 * view.scaleY), 
                Color.White);
        }
        public void Render(SpriteBatch spriteBatch, Sector sector, ViewController view)
        {
            
            foreach (System system in sector.Systems)
            {
                //Console.WriteLine("x " + system.x + " y " + system.y);
                if(system.hasLane)
                    DrawStar(spriteBatch, system,view,Color.White);
                
            } // 1075 916 1000 906
            foreach (WarpLane lane in sector.WarpLanes)
            {
                DrawLine(spriteBatch,
                    new Vector2(sector.Systems[lane.systemId1].x+40,
                                sector.Systems[lane.systemId1].y+40),
                    new Vector2(sector.Systems[lane.systemId2].x-40, 
                                sector.Systems[lane.systemId2].y-40),
                                Color.White);
            }
            //DrawLine(spriteBatch,
            //    new Vector2(500, 453),
            //     new Vector2(537, 458),
            //                 Color.White);
        }
    }
}
