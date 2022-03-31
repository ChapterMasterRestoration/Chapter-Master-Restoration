using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ChapterMaster
{
    class SectorRenderer
    {
        public SectorRenderer() { }
        Texture2D Pixel;
        PrimitiveBuddy.Primitive primitive;

        public void Initialize(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Pixel.SetData<Color>(new Color[] { Color.White });
            primitive = new PrimitiveBuddy.Primitive(graphicsDevice, spriteBatch);
        }

        // TODO: fix zoom and scale and camera transforms
        public void DrawLine(Vector2 start, Vector2 end, Color color, ViewController view)
        {
            primitive.Line(view.GetViewTransform(start), view.GetViewTransform(end), color);
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

        public void DrawStar(SpriteBatch spriteBatch, World.System system, Color color, ViewController view)
        {
            Rectangle rect = view.TransformedOriginRect(system.x, system.y, Constants.SystemSize, true);
            spriteBatch.Draw(Assets.SystemTextures[system.color],rect, Color.White);
            Rectangle rectLabel = view.TransformedOriginRect(system.x + Constants.SystemSize/4, system.y - Constants.SystemSize/4, Constants.SystemSize, true);
            Vector2 position = new Vector2(rectLabel.Left, rectLabel.Bottom);
            string name = system.name.Replace("’", "'");
            Vector2 nameSize = Assets.CourierNew.MeasureString(name);
            // TODO: scale system name better.
            spriteBatch.DrawString(Assets.CourierNew, name, position, Color.White, 0, new Vector2(0,0),view.zoom+0.3f, SpriteEffects.None,0);
            string owners = "";
            foreach(KeyValuePair<string, float> control in system.FindOwners())
            {
                owners += control.Key.Substring(0, 1) + $": {(control.Value * 100).ToString("G3")}%,";
            }
            spriteBatch.DrawString(Assets.CourierNew, owners, new Vector2(rectLabel.Left, rectLabel.Bottom + nameSize.Y * view.zoom + 1), Color.White, 0, new Vector2(0, 0), view.zoom + 0.3f, SpriteEffects.None, 0);
        }
        public void DrawFleet(SpriteBatch spriteBatch, Fleet.Fleet fleet, Color color, ViewController view, Sector sector)
        {
            // TODO: replace hardcoded offsets with constants.
            if (!fleet.isMoving)
            {
                List<Fleet.Fleet> orbitingFleets = new List<Fleet.Fleet>(0);
                foreach(Fleet.Fleet fleet1 in ChapterMaster.Sector.Fleets)
                {
                    if(fleet1.originSystemId == fleet.originSystemId)
                    {
                        orbitingFleets.Add(fleet1);
                    }
                }
                for (int orbitingFleetId = 0; orbitingFleetId < orbitingFleets.Count; orbitingFleetId++)
                {
                    Fleet.Fleet currentFleet = orbitingFleets[orbitingFleetId];
                    // TODO: the list of fleets should wrap down. and possibly scale down if there are too many.
                    spriteBatch.Draw(Assets.FleetTextures[currentFleet.fleetFaction][currentFleet.fleetState],
                                     view.TransformedOriginRect(
                                         ChapterMaster.Sector.Systems[currentFleet.originSystemId].x + 30 + (Constants.SystemSize / 2) * orbitingFleetId,
                                         ChapterMaster.Sector.Systems[currentFleet.originSystemId].y - 30,
                                         Constants.SystemSize,
                                         true),

                    Color.White);
                    if (orbitingFleets[orbitingFleetId].isSelected)
                    {
                       
                        primitive.Circle(view.GetViewTransform(
                                              ChapterMaster.Sector.Systems[fleet.originSystemId].x + 40 + 30 
                                              + (Constants.SystemSize / 2) * orbitingFleetId,
                                              ChapterMaster.Sector.Systems[fleet.originSystemId].y + 30 - 20),
                                         10 * view.zoom, Color.Green);
                    }
                }
            }
            else
            {
                // TODO: Improve this for cofleets moving together.
                // fleet.GetCofleetsMovingAlong()
                int travelTurns = ChapterMaster.Sector.CalculateTravelTurns(fleet);
                Vector2 oSystem = new Vector2(ChapterMaster.Sector.Systems[fleet.originSystemId].x + 30, ChapterMaster.Sector.Systems[fleet.originSystemId].y - 30);
                Vector2 dSystem = new Vector2(ChapterMaster.Sector.Systems[fleet.destinationSystemId].x, ChapterMaster.Sector.Systems[fleet.destinationSystemId].y);
                Vector2 Direction = (dSystem - oSystem) / travelTurns;
                Vector2 Position = oSystem + Direction * fleet.fleetMoveProgress;
                Rectangle rect = view.TransformedOriginRect(Position.X,
                                                            Position.Y,
                                                            Constants.SystemSize,
                                                            true);
                spriteBatch.Draw(Assets.FleetTextures[fleet.fleetFaction][fleet.fleetState], rect, Color.White);
                // TODO: replace this bs with trailing dashed line
                Color trailColor = Color.MediumPurple;
                int alpha = (byte)(255 * (1 - ((float)fleet.fleetMoveProgress / (float)travelTurns)));
                trailColor.A = (byte) alpha;
                DrawLine(oSystem + new Vector2(Constants.SystemSize / 2,
                                               Constants.SystemSize / 2),
                         Position + new Vector2(Constants.SystemSize / 2,
                                                Constants.SystemSize / 2),
                        trailColor, view);
                spriteBatch.DrawString(Assets.ARJULIAN, "ETA: " + (travelTurns - fleet.fleetMoveProgress), new Vector2(rect.Location.X+5,rect.Location.Y-5), Color.White);
                DrawDashedLine(Position + new Vector2(Constants.SystemSize / 2,
                                                      Constants.SystemSize / 2),
                               dSystem + new Vector2(Constants.SystemSize / 2,
                                                     Constants.SystemSize / 2),
                               10f, Color.White, view);
            }
        }
        public void Render(SpriteBatch spriteBatch, Sector sector, ViewController view)
        { 
            foreach (World.System system in ChapterMaster.Sector.Systems)
            {
                DrawStar(spriteBatch, system, Color.White, view);
            }
            foreach (WarpLane lane in ChapterMaster.Sector.WarpLanes)
            {
                DrawLine(
                    new Vector2(ChapterMaster.Sector.Systems[lane.systemId1].x + Constants.SystemSize / 2,
                                ChapterMaster.Sector.Systems[lane.systemId1].y + Constants.SystemSize / 2),
                    new Vector2(ChapterMaster.Sector.Systems[lane.systemId2].x + Constants.SystemSize / 2,
                                ChapterMaster.Sector.Systems[lane.systemId2].y + Constants.SystemSize / 2),
                                Color.White, view);
            }

            foreach (Fleet.Fleet fleet in ChapterMaster.Sector.Fleets)
            {
                DrawFleet(spriteBatch, fleet, Color.White, view, sector);
                if (fleet.isSelected)
                {



                    //primitive.Rectangle(new Rectangle(
                    //    (int)((ChapterMaster.Sector.Systems[view.currentSystemId].x + 30 - view.camX)
                    //    * view.zoom + ChapterMaster.GetWidth() / 2),
                    //    (int)((ChapterMaster.Sector.Systems[view.currentSystemId].x + 30 - view.camY)
                    //    * view.zoom + ChapterMaster.GetHeight() / 2),
                    //    (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleX * view.zoom / 2),
                    //    (int)(Constants.SYSTEM_WIDTH_HEIGHT * view.scaleY * view.zoom / 2)),
                    //    Color.Green);
                }

            }
            if (view.systemSelected)
            {
                primitive.Rectangle(new Rectangle(
                    (int)((ChapterMaster.Sector.Systems[view.currentSystemId].x - view.camX + Constants.SystemSize / 4)
                    * view.zoom + GameManager.GetWidth() / 2),
                    (int)((ChapterMaster.Sector.Systems[view.currentSystemId].y - view.camY + Constants.SystemSize / 4)
                    * view.zoom + GameManager.GetHeight() / 2),
                    (int)(Constants.SystemSize * view.scaleX * view.zoom / 2),
                    (int)(Constants.SystemSize * view.scaleY * view.zoom / 2)),
                    Color.Green);
            }
        }
    }
}