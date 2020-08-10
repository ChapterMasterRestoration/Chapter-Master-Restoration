using ChapterMaster.Util;
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
        
        // TODO: fix zoom and scale and camera transforms
        public void DrawLine(Vector2 start, Vector2 end, Color color, ViewController view)
        { 
           Vector2 camPositionTransform = new Vector2(view.camX, view.camY);
            // primitive.Line(Vector2.Transform(start,view.Transform), Vector2.Transform(end,view.Transform), color);
           primitive.Line((start - camPositionTransform)*view.zoom, (end-camPositionTransform)*view.zoom, color);
        }
        public void DrawStar(SpriteBatch spriteBatch, System system, Color color, ViewController view)
        {

            spriteBatch.Draw(ChapterMaster.SystemTextures[system.color],
                new Rectangle((int) ((system.x - view.camX)*view.zoom), (int) ((system.y - view.camY)*view.zoom),
                (int) (Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX * view.zoom), 
                (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY * view.zoom)),
                Color.White);
        }
        public void Render(SpriteBatch spriteBatch, Sector sector, ViewController view)
        {
            
            foreach (System system in sector.Systems) {
                DrawStar(spriteBatch, system, Color.White, view);
            } 
            foreach (WarpLane lane in sector.WarpLanes)
            {
                DrawLine(
                    new Vector2(sector.Systems[lane.systemId1].x+ Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                sector.Systems[lane.systemId1].y+ Constants.SYSTEM_WIDTH_HEIGHT / 2),
                    new Vector2(sector.Systems[lane.systemId2].x+ Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                sector.Systems[lane.systemId2].y+ Constants.SYSTEM_WIDTH_HEIGHT / 2),
                                Color.White,view);
            }
            
        }
    }
}
