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
        PrimitiveBuddy.Primitive primitive;

        public void Initialize(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Pixel = new Texture2D(ChapterMaster.graphicsDevice,1,1);
            Pixel.SetData<Color>(new Color[] { Color.White });
            primitive = new PrimitiveBuddy.Primitive(graphicsDevice, spriteBatch);
            
        }
        //public void DrawLine(SpriteBatch spriteBatch, Vector2 _start, Vector2 _end, Color color)
        //{
        //    Vector2 start = new Vector2(Math.Min(_start.X, _end.X), Math.Min(_start.Y, _end.Y));
        //    Vector2 end = new Vector2(Math.Max(_start.X, _end.X), Math.Max(_start.Y, _end.Y));
        //    double angle = Math.Atan2(end.Y-start.Y,end.X-start.X);
        //    spriteBatch.Draw(Pixel, null, new Rectangle((int)start.X, (int)start.Y, (int)end.Length(), 1),
        //        null, null, (float) angle,
        //        null, color, 
        //        SpriteEffects.None, 0);
        //}
        
        public void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            primitive.Line(start, end, color);
        }
        public void DrawStar(SpriteBatch spriteBatch, System system, ViewController view, Color color)
        {
            spriteBatch.Draw(ChapterMaster.SystemTextures[system.color], 
                new Rectangle(system.x, system.y, Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX, Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY), 
                Color.White);
        }
        public void Render(SpriteBatch spriteBatch, Sector sector, ViewController view)
        {
            
            foreach (System system in sector.Systems) {
                DrawStar(spriteBatch, system,view,Color.White);
            } // 1075 916 1000 906
            foreach (WarpLane lane in sector.WarpLanes)
            {
                DrawLine(
                    new Vector2(sector.Systems[lane.systemId1].x+ Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                sector.Systems[lane.systemId1].y+ Constants.SYSTEM_WIDTH_HEIGHT / 2),
                    new Vector2(sector.Systems[lane.systemId2].x+ Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                sector.Systems[lane.systemId2].y+ Constants.SYSTEM_WIDTH_HEIGHT / 2),
                                Color.White);
            }
            //DrawLine(spriteBatch,
            //    new Vector2(241, 342),
            //     new Vector2(246, 385),
            //                 Color.White);
        }
    }
}
