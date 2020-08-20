using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    public class ViewController
    {
        public int camX = 0;
        public int camY = 0;
        public float scaleX = 1;
        public float scaleY = 1;
        public float zoom = 1.0f;
        public int viewPortWidth;
        public int viewPortHeight;
        int _cameraSpeed = 3;
        public int currentSystemId;
        public bool systemSelected;
        public List<int> selectedFleets = new List<int>();
        public bool PlanetScreenOpen;
        //public Rectangle VisibleArea;
        //public Matrix Transform;
        public void UpdateKeyboard()
        {
            int cameraSpeed =(int) (_cameraSpeed / zoom);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camX += cameraSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camX -= cameraSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                camY -= cameraSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camY += cameraSpeed;
            }
            //CheckBoundaries();
        }
        int prevMouseWheelValue;
        int currentMouseWheelValue;
        public void UpdateMouse()
        {
            int cameraSpeed = (int)(_cameraSpeed / zoom);
            if (Mouse.GetState().X <= 0)
            {
                camX -= cameraSpeed;
            }
            if (Mouse.GetState().X >= ChapterMaster.GetWidth())
            {
                camX += cameraSpeed;
            }
            if (Mouse.GetState().Y <= 0)
            {
                camY -= cameraSpeed;
            }
            if (Mouse.GetState().Y >= ChapterMaster.GetHeight())
            {
                camY += cameraSpeed;
            }
            prevMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;
            if(currentMouseWheelValue > prevMouseWheelValue)
            {
                AdjustZoom(0.1f);
            }
            if (currentMouseWheelValue < prevMouseWheelValue)
            {
                AdjustZoom(-0.1f);
            }
            CheckBoundaries();
        }
        public void AdjustZoom(float factor)
        {
            zoom += factor;
            zoom = (float) Math.Round(Math.Abs(zoom), 1);
            //Debug.WriteLine("z " + zoom);
        }
        public void CheckBoundaries()
        { 
            if(camX * zoom < 0)
            {
                camX = 0;
            } 
            if(camY * zoom < 0)
            {
                camY = 0;
            }
            if (camX > Constants.WorldWidth / (2 * zoom))
            {
                camX = (int)(Constants.WorldWidth / (2 * zoom));
            }
            if (camY > Constants.WorldHeight / (2 * zoom))
            {
                camY = (int)(Constants.WorldHeight / (2 * zoom));
            }
        }

        // set up proper camera pipeline?

        //private void UpdateVisibleArea()
        //{
        //    var inverseViewMatrix = Matrix.Invert(Transform);

        //    var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
        //    var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
        //    var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
        //    var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

        //    var min = new Vector2(
        //        MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
        //        MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
        //    var max = new Vector2(
        //        MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
        //        MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
        //    VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        //}

        //private void UpdateMatrix()
        //{
        //    Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
        //            Matrix.CreateScale(Zoom) *
        //            Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
        //    UpdateVisibleArea();
        //}
        #region Transform Helpers
        public Vector2 GetViewTransform(int x, int y)
        {
            return new Vector2((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2), (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2));
        }

        /// <summary>
        /// This is transforms about the origin of the camera and scales and applies zoom to the coordinates given. 
        /// If enableZoom is true, then the Rectangle will also be scaled for zoom.
        /// </summary>
        public Rectangle TransformedOriginRect(int x, int y, int width, int height, bool enableZoom)
        {
            if (enableZoom)
            {
                return new Rectangle((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2),
                                     (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2),
                                     (int)(width * scaleX * zoom),
                                     (int)(height * scaleY * zoom));
            }
            else
            {
                return new Rectangle((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2),
                                     (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2),
                                     (int)(width * scaleX),
                                     (int)(height * scaleY));
            }
        }
        public Rectangle TransformedOriginRect(int x, int y, int size, bool enableZoom)
        {
            if (enableZoom)
            {
                return new Rectangle((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2),
                                     (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2),
                                     (int)(size * scaleX * zoom),
                                     (int)(size * scaleY * zoom));
            }
            else
            {
                return new Rectangle((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2),
                                     (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2),
                                     (int)(size * scaleX),
                                     (int)(size * scaleY));
            }
        }
        public Rectangle TransformedOriginRect(float x, float y, int size, bool enableZoom)
        {
            if (enableZoom)
            {
                return new Rectangle((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2),
                                     (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2),
                                     (int)(size * scaleX * zoom),
                                     (int)(size * scaleY * zoom));
            }
            else
            {
                return new Rectangle((int)((x - camX) * zoom + ChapterMaster.GetWidth() / 2),
                                     (int)((y - camY) * zoom + ChapterMaster.GetHeight() / 2),
                                     (int)(size * scaleX),
                                     (int)(size * scaleY));
            }
        }
        #endregion
        public void Update() { 
        }

        int DeselectionDelay = 400;
        int delayTimer;
        int openSystem = -1;
        // TODO: create list of moused-over systems and use that to disable no-shift clear
        public void MouseSelection(Sector sector)
        {
            int mouseX = Mouse.GetState().X;
            int mouseY = Mouse.GetState().Y;
            #region System Selection
            for (int systemId = 0; systemId < sector.Systems.Count; systemId++)
            {
                /*  Vector2 camPositionTransform = new Vector2(camX, camY);
                  Vector2 originTransform = new Vector2(ChapterMaster.GetWidth() / 2, ChapterMaster.GetHeight() / 2);
                  Vector2 upperLeft = (new Vector2(system.x,system.y) - camPositionTransform) * zoom + originTransform;
                  upperLeft, upperLeft + (new Vector2(Constants.SYSTEM_WIDTH_HEIGHT, Constants.SYSTEM_WIDTH_HEIGHT) * zoom + originTransform) */

                // TODO: replace with Rectangle.Contains
                int ulCornerX = (int)((sector.Systems[systemId].x - camX) * zoom + ChapterMaster.GetWidth() / 2);
                int ulCornerY = (int)((sector.Systems[systemId].y - camY) * zoom + ChapterMaster.GetHeight() / 2);
                int brCornerX = (int)((sector.Systems[systemId].x + Constants.SystemSize / 2 - camX) * zoom + ChapterMaster.GetWidth() / 2);
                int brCornerY = (int)((sector.Systems[systemId].y + Constants.SystemSize / 2 - camY) * zoom + ChapterMaster.GetHeight() / 2);
                if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
                {
                    currentSystemId = systemId;
                    systemSelected = true;
                    if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        foreach (int id in selectedFleets)
                        {
                            if (!sector.Fleets[id].isMoving && sector.Fleets[id].originSystemId != currentSystemId)
                            {
                                sector.Fleets[id].destinationSystemId = currentSystemId;
                                sector.Fleets[id].isMoving = true;
                                sector.Fleets[id].fleetMoveProgress = 0;
                            }
                        }
                    } else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (!PlanetScreenOpen)
                        {
                            sector.Systems[currentSystemId].OpenPlanetsScreen(this,currentSystemId);
                            PlanetScreenOpen = true;
                            openSystem = currentSystemId;
                        }
                    }
                } else
                {
                    delayTimer++;
                    if(delayTimer > DeselectionDelay) {
                        systemSelected = false;
                        delayTimer = 0;
                    }
                    //PlanetScreenWasOpen = false;
                    //sector.Systems[currentSystemId].ClosePlanetScreen(this);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && systemId == openSystem)
                    {
                        Debug.WriteLine("Closing planet screen");
                        Predicate<UI.Screen> predicate = delegate (UI.Screen screen) { return screen is UI.PlanetsScreen; };
                        ChapterMaster.MainScreen.Screens.RemoveAll(predicate);
                        PlanetScreenOpen = false;
                        openSystem = -1;
                    }
                }
            }
            #endregion
            #region Fleet Selection

            for (int fleetId = 0; fleetId < sector.Fleets.Count; fleetId++)
            {
                int systemId = sector.Fleets[fleetId].originSystemId;
                sector.Fleets[fleetId].fleetId = fleetId; // ???
                sector.Fleets[fleetId].checkedByCoFleet = false;
                sector.Fleets[fleetId].coFleets.Clear();
                List <Fleet.Fleet> orbitingFleets = new List<Fleet.Fleet>();
                for(int oFleetId = 0; oFleetId < sector.Fleets.Count; oFleetId++)
                {
                    if (sector.Fleets[oFleetId].originSystemId == systemId)
                    {
                        if (!sector.Fleets[fleetId].coFleets.Contains(oFleetId))
                        {
                            sector.Fleets[oFleetId].fleetId = oFleetId; // TODO: will this create problems when the list of fleets changes?
                            orbitingFleets.Add(sector.Fleets[oFleetId]);
                            if(oFleetId != fleetId)
                                sector.Fleets[fleetId].coFleets.Add(oFleetId);
                        }
                    }
                }
                for(int orbitingFleetId = 0; orbitingFleetId < orbitingFleets.Count; orbitingFleetId++)
                {
                    if (orbitingFleets[orbitingFleetId].coFleets.Contains(fleetId))
                    {
                        orbitingFleets[orbitingFleetId].checkedByCoFleet = true;
                    }
                    int ulCornerX = (int)((sector.Systems[systemId].x + (Constants.SystemSize / 4) + 30 - camX) * zoom + ChapterMaster.GetWidth() / 2);
                    int ulCornerY = (int)((sector.Systems[systemId].y + (Constants.SystemSize / 4) - 30 - camY) * zoom + ChapterMaster.GetHeight() / 2);
                    int brCornerX = (int)((sector.Systems[systemId].x + (Constants.SystemSize / 4) + 30 + Constants.SystemSize / 2 - camX) * zoom + ChapterMaster.GetWidth() / 2);
                    int brCornerY = (int)((sector.Systems[systemId].y + (Constants.SystemSize / 4) - 30 + Constants.SystemSize / 2 - camY) * zoom + ChapterMaster.GetHeight() / 2);
                    int fleetWidth = brCornerX - ulCornerX;
                    ulCornerX = ulCornerX + fleetWidth * orbitingFleetId;
                    brCornerX = brCornerX + fleetWidth * orbitingFleetId;
                    bool isOverSystem = false;
                    //Debug.WriteLine("checking fleet " + orbitingFleets[orbitingFleetId].fleetId + " with " + orbitingFleets.Count() + " others");
                    if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY && !orbitingFleets[orbitingFleetId].checkedByCoFleet)
                    {
                        isOverSystem = true;
                        //currentSystemId = systemId;
                        //systemSelected = true;
                        if (!orbitingFleets[orbitingFleetId].checkedByCoFleet)
                        {
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                //Debug.WriteLine("pressed over fleet " + orbitingFleets[orbitingFleetId].fleetId);
                                if (!selectedFleets.Contains(orbitingFleets[orbitingFleetId].fleetId))
                                {
                                    selectedFleets.Add(orbitingFleets[orbitingFleetId].fleetId);
                                    Debug.WriteLine("selected fleet " + orbitingFleets[orbitingFleetId].fleetId + " doing " + fleetId);
                                    sector.Fleets[orbitingFleets[orbitingFleetId].fleetId].isSelected = true;

                                }
                                else
                                {
                                    selectedFleets.Remove(orbitingFleets[orbitingFleetId].fleetId);
                                    sector.Fleets[orbitingFleets[orbitingFleetId].fleetId].isSelected = false;
                                }
                            }
                        }
                    }
                    else if (Mouse.GetState().LeftButton == ButtonState.Pressed && 
                            !Keyboard.GetState().IsKeyDown(Keys.LeftShift) &&
                            !sector.Fleets[fleetId].coFleets.Contains(fleetId) && !isOverSystem)
                    {
                        foreach (int id in selectedFleets)
                        {
                            sector.Fleets[id].isSelected = false;
                        }
                        selectedFleets.Clear();
                        isOverSystem = false;
                    }
                }

            }
            #endregion
            string fleets = "";
            foreach (int id in selectedFleets)
            {
                fleets += "F"+id + ", ";
                fleets += "\n";
                fleets += "COF: ";
                foreach(int coId in sector.Fleets[id].coFleets)
                {
                    fleets += coId + ", ";
                }

            }
            ChapterMaster.DebugString = "System: " + currentSystemId + "\n" + "Fleet: " + fleets;
        }
        public MouseState GetMouse()
        {
            return Mouse.GetState();
        }
    }
}
