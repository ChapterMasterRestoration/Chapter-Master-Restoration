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
            Pixel = new Texture2D(ChapterMaster.graphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });
            primitive = new PrimitiveBuddy.Primitive(graphicsDevice, spriteBatch);

        }

        // TODO: fix zoom and scale and camera transforms
        public void DrawLine(Vector2 start, Vector2 end, Color color, ViewController view)
        {
            Vector2 camPositionTransform = new Vector2(view.camX, view.camY);
            Vector2 originTransform = new Vector2(ChapterMaster.GetWidth() / 2, ChapterMaster.GetHeight() / 2);
            // primitive.Line(Vector2.Transform(start,view.Transform), Vector2.Transform(end,view.Transform), color);
            primitive.Line((start - camPositionTransform) * view.zoom + originTransform, (end - camPositionTransform) * view.zoom + originTransform, color);
        }
        public void DrawStar(SpriteBatch spriteBatch, System system, Color color, ViewController view)
        {

            spriteBatch.Draw(ChapterMaster.SystemTextures[system.color],
                new Rectangle((int)((system.x - view.camX) * view.zoom + ChapterMaster.GetWidth() / 2), (int)((system.y - view.camY) * view.zoom + ChapterMaster.GetHeight() / 2),
                (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX * view.zoom),
                (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY * view.zoom)),
                Color.White);
        }
        public void DrawFleet(SpriteBatch spriteBatch, Fleet.Fleet fleet, Color color, ViewController view, Sector sector)
        {
            spriteBatch.Draw(ChapterMaster.FleetTextures[fleet.fleetFaction][fleet.fleetState],
                new Rectangle((int)((sector.Systems[fleet.originSystemId].x + 30 - view.camX) * view.zoom + ChapterMaster.GetWidth() / 2),
                (int)((sector.Systems[fleet.originSystemId].y - 30 - view.camY) * view.zoom + ChapterMaster.GetHeight() / 2),
                (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX * view.zoom),
                (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY * view.zoom)),
                Color.White);

        }
        public void Render(SpriteBatch spriteBatch, Sector sector, ViewController view)
        {

            foreach (System system in sector.Systems)
            {
                DrawStar(spriteBatch, system, Color.White, view);
            }
            foreach (WarpLane lane in sector.WarpLanes)
            {
                DrawLine(
                    new Vector2(sector.Systems[lane.systemId1].x + Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                sector.Systems[lane.systemId1].y + Constants.SYSTEM_WIDTH_HEIGHT / 2),
                    new Vector2(sector.Systems[lane.systemId2].x + Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                sector.Systems[lane.systemId2].y + Constants.SYSTEM_WIDTH_HEIGHT / 2),
                                Color.White, view);
            }

            foreach (Fleet.Fleet fleet in sector.Fleets)
            {
                DrawFleet(spriteBatch, fleet, Color.White, view, sector);

            }
            if (view.systemSelected)
            {
                primitive.Rectangle(new Rectangle(
                    (int)((sector.Systems[view.currentSystemId].x - view.camX + Constants.SYSTEM_WIDTH_HEIGHT / 4)
                    * view.zoom + ChapterMaster.GetWidth() / 2),
                    (int)((sector.Systems[view.currentSystemId].y - view.camY + Constants.SYSTEM_WIDTH_HEIGHT / 4)
                    * view.zoom + ChapterMaster.GetHeight() / 2),
                    (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX * view.zoom / 2),
                    (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY * view.zoom / 2)),
                    Color.Green);
            }
        }
    }
}