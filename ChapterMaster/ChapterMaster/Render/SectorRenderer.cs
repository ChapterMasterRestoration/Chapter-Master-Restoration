using ChapterMaster.Util;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            // idk what to do about this transform.
            Vector2 camPositionTransform = new Vector2(view.camX, view.camY);
            Vector2 originTransform = new Vector2(ChapterMaster.GetWidth() / 2, ChapterMaster.GetHeight() / 2);
            // primitive.Line(Vector2.Transform(start,view.Transform), Vector2.Transform(end,view.Transform), color);
            primitive.Line((start - camPositionTransform) * view.zoom + originTransform, (end - camPositionTransform) * view.zoom + originTransform, color);
        }
        public void DrawDashedLine(Vector2 start, Vector2 end, float thickness, Color color, ViewController view)
        {
            Vector2 Direction = end - start;
            int segments = (int)Math.Round(Direction.Length() / thickness);
            Vector2 step = Direction / segments;
            for (int i = 0; i < segments; i++)
            {
                if (i % 2 == 0)
                {
                    DrawLine(start + step * i, (start + step * i) + (step), color, view);
                }
            }
        }

        public void DrawStar(SpriteBatch spriteBatch, System system, Color color, ViewController view)
        {
            spriteBatch.Draw(ChapterMaster.SystemTextures[system.color],
                view.TransformedOriginRect(system.x, system.y, Constants.SYSTEM_WIDTH_HEIGHT, true),
                Color.White);
        }
        public void DrawFleet(SpriteBatch spriteBatch, Fleet.Fleet fleet, Color color, ViewController view, Sector sector)
        {
            // TODO: replace hardcoded offsets with constants.
            if (!fleet.isMoving)
            {
                spriteBatch.Draw(ChapterMaster.FleetTextures[fleet.fleetFaction][fleet.fleetState],
                                 view.TransformedOriginRect(
                                     sector.Systems[fleet.originSystemId].x + 30,
                                     sector.Systems[fleet.originSystemId].y - 30,
                                     Constants.SYSTEM_WIDTH_HEIGHT,
                                     true),
                Color.White);
            }
            else
            {
                int travelTurns = sector.CalculateTravelTurns(fleet);
                Vector2 oSystem = new Vector2(sector.Systems[fleet.originSystemId].x + 30, sector.Systems[fleet.originSystemId].y - 30);
                Vector2 dSystem = new Vector2(sector.Systems[fleet.destinationSystemId].x, sector.Systems[fleet.destinationSystemId].y);
                Vector2 Direction = (dSystem - oSystem) / travelTurns;
                Vector2 Position = oSystem + Direction * fleet.fleetMoveProgress;
                spriteBatch.Draw(ChapterMaster.FleetTextures[fleet.fleetFaction][fleet.fleetState],
                                 view.TransformedOriginRect(Position.X,
                                                            Position.Y,
                                                            Constants.SYSTEM_WIDTH_HEIGHT,
                                                            true),
                                 Color.White);
                // replace this bs with trailing dashed line
                Color trailColor = Color.MediumPurple;
                int alpha = (byte)(255 * (1 - ((float)fleet.fleetMoveProgress / (float)travelTurns)));
                trailColor.A = (byte) alpha;
                DrawLine(oSystem + new Vector2(Constants.SYSTEM_WIDTH_HEIGHT / 2 ,
                                               Constants.SYSTEM_WIDTH_HEIGHT / 2 ),
                         Position + new Vector2(Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                                Constants.SYSTEM_WIDTH_HEIGHT / 2),
                        trailColor, view);
                DrawDashedLine(Position + new Vector2(Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                                      Constants.SYSTEM_WIDTH_HEIGHT / 2),
                               dSystem + new Vector2(Constants.SYSTEM_WIDTH_HEIGHT / 2,
                                                     Constants.SYSTEM_WIDTH_HEIGHT / 2),
                               10f, Color.White, view);
            }
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
                if (fleet.isSelected)
                {


                    primitive.Circle(
                        new Vector2((sector.Systems[fleet.originSystemId].x + 40 + 30 - view.camX)
                                    * view.zoom + ChapterMaster.GetWidth() / 2,
                                    (sector.Systems[fleet.originSystemId].y + 30 - 20 - view.camY)
                                    * view.zoom + ChapterMaster.GetHeight() / 2),
                        10 * view.zoom, Color.Green);


                    //primitive.Rectangle(new Rectangle(
                    //    (int)((sector.Systems[view.currentSystemId].x + 30 - view.camX)
                    //    * view.zoom + ChapterMaster.GetWidth() / 2),
                    //    (int)((sector.Systems[view.currentSystemId].x + 30 - view.camY)
                    //    * view.zoom + ChapterMaster.GetHeight() / 2),
                    //    (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX * view.zoom / 2),
                    //    (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY * view.zoom / 2)),
                    //    Color.Green);
                }

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