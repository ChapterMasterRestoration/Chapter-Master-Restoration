using ChapterMaster.UI.Align;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        int _cameraSpeed = 4;
        public int currentSystemId;
        public bool systemSelected;
        public List<int> selectedFleets = new List<int>();
        public bool PlanetScreenOpen;
        public bool IsOccluded;
        SystemScreenAlign currentSystemScreenAlign;
        //public Rectangle VisibleArea;
        //public Matrix Transform;
        public virtual void UpdateKeyboard()
        {
            int cameraSpeed = (int)(_cameraSpeed / zoom);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camX += cameraSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camX -= cameraSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
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
        public virtual void UpdateMouse()
        {
            int cameraSpeed = (int)(_cameraSpeed / zoom);
            if (Mouse.GetState().X <= 0)
            {
                camX -= cameraSpeed;
            }
            if (Mouse.GetState().X >= GameManager.GetWidth())
            {
                camX += cameraSpeed;
            }
            if (Mouse.GetState().Y <= 0)
            {
                camY -= cameraSpeed;
            }
            if (Mouse.GetState().Y >= GameManager.GetHeight())
            {
                camY += cameraSpeed;
            }
            prevMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;
            if (currentMouseWheelValue > prevMouseWheelValue)
            {
                AdjustZoom(0.1f);
            }
            if (currentMouseWheelValue < prevMouseWheelValue)
            {
                AdjustZoom(-0.1f);
            }
            CheckBoundaries();
        }
        public virtual void AdjustZoom(float factor)
        {
            zoom += factor;
            zoom = (float)Math.Round(Math.Abs(zoom), 1);
            //Debug.WriteLine("z " + zoom);
        }
        public virtual void CheckBoundaries()
        {
            if (camX * zoom < 0)
            {
                camX = 0;
            }
            if (camY * zoom < 0)
            {
                camY = 0;
            }
            if (camX > Constants.WorldWidth)
            {
                camX = (int)(Constants.WorldWidth);
            }
            if (camY > Constants.WorldHeight)
            {
                camY = (int)(Constants.WorldHeight);
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
            return new Vector2((int)((x - camX) * zoom + GameManager.GetWidth() / 2), (int)((y - camY) * zoom + GameManager.GetHeight() / 2));
        }

        /// <summary>
        /// This is transforms about the origin of the camera and scales and applies zoom to the coordinates given. 
        /// If enableZoom is true, then the Rectangle will also be scaled for zoom.
        /// </summary>
        public Rectangle TransformedOriginRect(int x, int y, int width, int height, bool enableZoom)
        {
            if (enableZoom)
            {
                return new Rectangle((int)((x - camX) * zoom + GameManager.GetWidth() / 2),
                                     (int)((y - camY) * zoom + GameManager.GetHeight() / 2),
                                     (int)(width * scaleX * zoom),
                                     (int)(height * scaleY * zoom));
            }
            else
            {
                return new Rectangle((int)((x - camX) * zoom + GameManager.GetWidth() / 2),
                                     (int)((y - camY) * zoom + GameManager.GetHeight() / 2),
                                     (int)(width * scaleX),
                                     (int)(height * scaleY));
            }
        }
        public Rectangle TransformedOriginRect(int x, int y, int size, bool enableZoom)
        {
            if (enableZoom)
            {
                return new Rectangle((int)((x - camX) * zoom + GameManager.GetWidth() / 2),
                                     (int)((y - camY) * zoom + GameManager.GetHeight() / 2),
                                     (int)(size * scaleX * zoom),
                                     (int)(size * scaleY * zoom));
            }
            else
            {
                return new Rectangle((int)((x - camX) * zoom + GameManager.GetWidth() / 2),
                                     (int)((y - camY) * zoom + GameManager.GetHeight() / 2),
                                     (int)(size * scaleX),
                                     (int)(size * scaleY));
            }
        }
        public Rectangle TransformedOriginRect(float x, float y, int size, bool enableZoom)
        {
            if (enableZoom)
            {
                return new Rectangle((int)((x - camX) * zoom + GameManager.GetWidth() / 2),
                                     (int)((y - camY) * zoom + GameManager.GetHeight() / 2),
                                     (int)(size * scaleX * zoom),
                                     (int)(size * scaleY * zoom));
            }
            else
            {
                return new Rectangle((int)((x - camX) * zoom + GameManager.GetWidth() / 2),
                                     (int)((y - camY) * zoom + GameManager.GetHeight() / 2),
                                     (int)(size * scaleX),
                                     (int)(size * scaleY));
            }
        }
        #endregion
        public void Update()
        {
        }

        int DeselectionDelay = 400;
        int delayTimer;
        public int openSystem = -1; // screw it, i'll leave it here for now
        ButtonState previousLMBState; // Left Mouse Button for you uninitiated, uncultured reactionist neo-liberals.
        // TODO: create list of moused-over systems and use that to disable no-shift clear
        public virtual void MouseSelection(Sector sector)
        {
            int mouseX = Mouse.GetState().X;
            int mouseY = Mouse.GetState().Y;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //Debug.WriteLine($"x: {mouseX} y: {mouseY}");
            }
            #region System Selection
            if (!IsOccluded)
            {

                for (int systemId = 0; systemId < ChapterMaster.Sector.Systems.Count; systemId++)
                {
                    /*  Vector2 camPositionTransform = new Vector2(camX, camY);
                      Vector2 originTransform = new Vector2(ChapterMaster.GetWidth() / 2, ChapterMaster.GetHeight() / 2);
                      Vector2 upperLeft = (new Vector2(system.x,system.y) - camPositionTransform) * zoom + originTransform;
                      upperLeft, upperLeft + (new Vector2(Constants.SYSTEM_WIDTH_HEIGHT, Constants.SYSTEM_WIDTH_HEIGHT) * zoom + originTransform) */

                    // TODO: replace with Rectangle.Contains
                    int ulCornerX = (int)((ChapterMaster.Sector.Systems[systemId].x - camX) * zoom + GameManager.GetWidth() / 2);
                    int ulCornerY = (int)((ChapterMaster.Sector.Systems[systemId].y - camY) * zoom + GameManager.GetHeight() / 2);
                    int brCornerX = (int)((ChapterMaster.Sector.Systems[systemId].x + Constants.SystemSize / 2 - camX) * zoom + GameManager.GetWidth() / 2);
                    int brCornerY = (int)((ChapterMaster.Sector.Systems[systemId].y + Constants.SystemSize / 2 - camY) * zoom + GameManager.GetHeight() / 2);
                    if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
                    {
                        currentSystemId = systemId;
                        systemSelected = true;
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            foreach (int id in selectedFleets)
                            {
                                if (!ChapterMaster.Sector.Fleets[id].isMoving && ChapterMaster.Sector.Fleets[id].originSystemId != currentSystemId)
                                {
                                    ChapterMaster.Sector.Fleets[id].destinationSystemId = currentSystemId;
                                    ChapterMaster.Sector.Fleets[id].isMoving = true;
                                    ChapterMaster.Sector.Fleets[id].fleetMoveProgress = 0;
                                }
                            }
                        }
                        else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            if (!PlanetScreenOpen)
                            {
                                currentSystemScreenAlign = (SystemScreenAlign)ChapterMaster.Sector.Systems[currentSystemId].OpenSystemScreen(this, currentSystemId).align;
                                PlanetScreenOpen = true;
                                openSystem = currentSystemId;
                            }
                        }
                    }
                    else
                    {
                        delayTimer++;
                        if (delayTimer > DeselectionDelay)
                        {
                            systemSelected = false;
                            delayTimer = 0;
                        }
                        #region Moved To SystemScreen
                        /*
                        //PlanetScreenWasOpen = false;
                        //ChapterMaster.Sector.Systems[currentSystemId].CloseSystemScreen(this);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && systemId == openSystem)
                            if (currentSystemScreenAlign.GetRect(this).Contains(new Point(mouseX, mouseY)))
                            {
                                //Debug.WriteLine("Closing planet screen"); 
                                for(int i = 0; i < ChapterMaster.MainScreen.Screens.Count; i++)
                                {
                                    if (ChapterMaster.MainScreen.Screens[i] is SystemScreen)
                                    {
                                       // Predicate<UI.Screen> predicate = delegate (UI.Screen screen) { return screen is UI.PlanetScreen; };
                                        //ChapterMaster.MainScreen.Screens[i].Screens.RemoveAll(predicate);
                                    }
                                }

                            }
                            else 
                            {
                               // Debug.WriteLine("Closing system screen");
                               // Predicate<UI.Screen> predicate = delegate (UI.Screen screen) { return screen is UI.SystemScreen; };
                                //ChapterMaster.MainScreen.Screens.RemoveAll(predicate);
                               // PlanetScreenOpen = false;
                                //openSystem = -1;
                            }
                            */
                        #endregion
                    }
                }
            }
            #endregion
            #region Old Fleet Selection
            // TODO: rewrite all this crap using Fleet.Intersect
            /*
            for (int fleetId = 0; fleetId < ChapterMaster.Sector.Fleets.Count; fleetId++)
            {
                int systemId = ChapterMaster.Sector.Fleets[fleetId].originSystemId;
                ChapterMaster.Sector.Fleets[fleetId].fleetId = fleetId; // ???
                ChapterMaster.Sector.Fleets[fleetId].checkedByCoFleet = false;
                ChapterMaster.Sector.Fleets[fleetId].coFleets.Clear();
                List<Fleet.Fleet> orbitingFleets = new List<Fleet.Fleet>();
                for (int oFleetId = 0; oFleetId < ChapterMaster.Sector.Fleets.Count; oFleetId++)
                {
                    if (ChapterMaster.Sector.Fleets[oFleetId].originSystemId == systemId)
                    {
                        if (!ChapterMaster.Sector.Fleets[fleetId].coFleets.Contains(oFleetId))
                        {
                            ChapterMaster.Sector.Fleets[oFleetId].fleetId = oFleetId; // TODO: will this create problems when the list of fleets changes?
                            orbitingFleets.Add(ChapterMaster.Sector.Fleets[oFleetId]);
                            if (oFleetId != fleetId)
                                ChapterMaster.Sector.Fleets[fleetId].coFleets.Add(oFleetId);
                        }
                    }
                }
                for (int orbitingFleetId = 0; orbitingFleetId < orbitingFleets.Count; orbitingFleetId++)
                {
                    if (orbitingFleets[orbitingFleetId].coFleets.Contains(fleetId))
                    {
                        orbitingFleets[orbitingFleetId].checkedByCoFleet = true;
                    }
                    int ulCornerX = (int)((ChapterMaster.Sector.Systems[systemId].x + (Constants.SystemSize / 4) + 30 - camX) * zoom + ChapterMaster.GetWidth() / 2);
                    int ulCornerY = (int)((ChapterMaster.Sector.Systems[systemId].y + (Constants.SystemSize / 4) - 30 - camY) * zoom + ChapterMaster.GetHeight() / 2);
                    int brCornerX = (int)((ChapterMaster.Sector.Systems[systemId].x + (Constants.SystemSize / 4) + 30 + Constants.SystemSize / 2 - camX) * zoom + ChapterMaster.GetWidth() / 2);
                    int brCornerY = (int)((ChapterMaster.Sector.Systems[systemId].y + (Constants.SystemSize / 4) - 30 + Constants.SystemSize / 2 - camY) * zoom + ChapterMaster.GetHeight() / 2);
                    int fleetWidth = brCornerX - ulCornerX;
                    ulCornerX = ulCornerX + fleetWidth * orbitingFleetId;
                    brCornerX = brCornerX + fleetWidth * orbitingFleetId;
                    bool isOverSystem = false;
                    //Debug.WriteLine("checking fleet " + orbitingFleets[orbitingFleetId].fleetId + " with " + orbitingFleets.Count() + " others");
                    if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY && !orbitingFleets[orbitingFleetId].checkedByCoFleet)
                    {
                        //isOverSystem = true;
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
                                    ChapterMaster.Sector.Fleets[orbitingFleets[orbitingFleetId].fleetId].isSelected = true;

                                }
                                else
                                {
                                    Debug.WriteLine("deselecting fleet id" + orbitingFleetId);
                                    selectedFleets.Remove(orbitingFleets[orbitingFleetId].fleetId);
                                    ChapterMaster.Sector.Fleets[orbitingFleets[orbitingFleetId].fleetId].isSelected = false;
                                }
                            }
                        }
                    }
                    else if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                            !Keyboard.GetState().IsKeyDown(Keys.LeftShift) &&
                            !ChapterMaster.Sector.Fleets[fleetId].coFleets.Contains(fleetId) && !isOverSystem &&
                            !ChapterMaster.Sector.Fleets[fleetId].checkedByCoFleet)
                    {
                        bool overFleet = false;
                        for (int i = 0; i < ChapterMaster.Sector.Fleets.Count; i++)
                        {
                            if (ChapterMaster.Sector.Fleets[i].Intersects(this))
                            {
                                overFleet = true;
                            }
                            else
                            {

                            }
                        }
                        if (!overFleet)
                        {
                            Debug.WriteLine("trying to deselect ");
                            for (int i = 0; i < ChapterMaster.Sector.Fleets.Count; i++)
                            {
                                ChapterMaster.Sector.Fleets[i].isSelected = false;
                                Debug.WriteLine("deselecting " + i);
                            }
                            selectedFleets.Clear();
                        }
                    }
                    isOverSystem = false;
                }
            }
            */
            #endregion
            #region Fleet Selection
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && previousLMBState == ButtonState.Released && !IsOccluded)
            {
                bool notWasOverFleet = true;
                for (int fleetId = 0; fleetId < ChapterMaster.Sector.Fleets.Count; fleetId++)
                {
                    Debug.WriteLine($"Fleet ID {fleetId}");
                    if (ChapterMaster.Sector.Fleets[fleetId].Intersects(this))
                    {
                        if (!selectedFleets.Contains(fleetId))
                        {
                            selectedFleets.Add(fleetId);
                            ChapterMaster.Sector.Fleets[fleetId].isSelected = true;
                            notWasOverFleet = false;
                        }
                        else
                        {
                            selectedFleets.Remove(fleetId);
                            ChapterMaster.Sector.Fleets[fleetId].isSelected = false;
                        }

                    }
                    else
                    {
                        for (int otherFleetId = 0; otherFleetId < ChapterMaster.Sector.Fleets.Count; otherFleetId++)
                        {
                            if (ChapterMaster.Sector.Fleets[fleetId].Intersects(this))
                            {
                                if (otherFleetId != fleetId)
                                {
                                    //selectedFleets.Remove(otherFleetId);
                                    //ChapterMaster.Sector.Fleets[otherFleetId].isSelected = false;
                                }
                                //notWasOverFleet = false;
                            }
                            else
                            {

                            }
                        }
                    }
                }
                if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    //for (int fleetToDeselect = 0; fleetToDeselect < selectedFleets.Count; fleetToDeselect++)
                    //{
                    //    if (!ChapterMaster.Sector.Fleets[selectedFleets[fleetToDeselect]].Intersects(this))
                    //    {
                    //        Debug.WriteLine($"noshift deselecting fleet {fleetToDeselect}");
                    //        ChapterMaster.Sector.Fleets[selectedFleets[fleetToDeselect]].isSelected = false;
                    //        selectedFleets.Remove(selectedFleets[fleetToDeselect]);
                    //    }
                    //}
                    // finally after weeks the fleet selection bug has been solved!! 
                    // this is proof that the socialist revolution can happen!
                    // marx is a god! praise Bozhe and marx and the Tsar and the Omnissiah.
                    // kill stalin and hitler!
                    // btw lenin sucks.
                    // i'm not even sure how this works.
                    for (int fleetToDeselect = 0; fleetToDeselect < ChapterMaster.Sector.Fleets.Count; fleetToDeselect++)
                    {
                        if (!ChapterMaster.Sector.Fleets[fleetToDeselect].Intersects(this))
                        {
                            Debug.WriteLine($"noshift deselecting fleet {fleetToDeselect}");
                            ChapterMaster.Sector.Fleets[fleetToDeselect].isSelected = false;
                            selectedFleets.Remove(fleetToDeselect);
                        }
                    }
                }
            }
            previousLMBState = Mouse.GetState().LeftButton;
            #endregion
            string fleets = "";
            foreach (int id in selectedFleets)
            {
                fleets += "F" + id + ", ";
                fleets += "\n";
                fleets += "COF: ";
                foreach (int coId in ChapterMaster.Sector.Fleets[id].coFleets)
                {
                    fleets += coId + ", ";
                }
            }
            ChapterMaster.DebugString = "System: " + currentSystemId + "\n" + "Fleet: " + fleets;
            IsOccluded = false;
        }
        public MouseState GetMouse()
        {
            return Mouse.GetState();
        }
    }
}
