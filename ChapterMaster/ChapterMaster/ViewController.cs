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
    class ViewController
    {
        public int camX = 0;
        public int camY = 0;
        public int scaleX = 1;
        public int scaleY = 1;
        public float zoom = 1.0f;
        int _cameraSpeed = 3;
        public int currentSystemId;
        public bool systemSelected;
        
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
            //CheckBoundaries();
        }
        public void AdjustZoom(float factor)
        {
            zoom += factor;
            zoom = (float) Math.Round(Math.Abs(zoom), 1);
            //Debug.WriteLine("z " + zoom);
        }
        public void CheckBoundaries()
        { 
            if(camX < 0)
            {
                camX = 0;
            } 
            if(camY < 0)
            {
                camY = 0;
            }
            if (camX > Constants.WorldWidth/2)
            {
                camX = Constants.WorldWidth/2;
            }
            if (camY > Constants.WorldHeight / 2)
            {
                camY = Constants.WorldHeight / 2;
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

        public void Update() { 
        }

        int DeselectionDelay = 400;
        int delayTimer;
        public void MouseSelection(Sector sector)
        {
            for(int systemId = 0; systemId < sector.Systems.Count; systemId++)
            {
                /*  Vector2 camPositionTransform = new Vector2(camX, camY);
                  Vector2 originTransform = new Vector2(ChapterMaster.GetWidth() / 2, ChapterMaster.GetHeight() / 2);
                  Vector2 upperLeft = (new Vector2(system.x,system.y) - camPositionTransform) * zoom + originTransform;
                  upperLeft, upperLeft + (new Vector2(Constants.SYSTEM_WIDTH_HEIGHT, Constants.SYSTEM_WIDTH_HEIGHT) * zoom + originTransform) */
                int mouseX = Mouse.GetState().X;
                int mouseY = Mouse.GetState().Y;
                // TODO: replace with Rectangle.Contains
                int ulCornerX = (int)((sector.Systems[systemId].x - camX) * zoom + ChapterMaster.GetWidth() / 2);
                int ulCornerY = (int)((sector.Systems[systemId].y - camY) * zoom + ChapterMaster.GetHeight() / 2);
                int brCornerX = (int)((sector.Systems[systemId].x + Constants.SYSTEM_WIDTH_HEIGHT / 2 - camX) * zoom + ChapterMaster.GetWidth() / 2);
                int brCornerY = (int)((sector.Systems[systemId].y + Constants.SYSTEM_WIDTH_HEIGHT / 2 - camY) * zoom + ChapterMaster.GetHeight() / 2);
                if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
                {
                    currentSystemId = systemId;
                    systemSelected = true;
                    if(Mouse.GetState().RightButton == ButtonState.Pressed)
                    {

                    }
                } else
                {
                    delayTimer++;
                    if(delayTimer > DeselectionDelay) {
                        systemSelected = false;
                        delayTimer = 0;
                    }
                }
            }
           
            // check buttons
        }
        public MouseState GetMouse()
        {
            return Mouse.GetState();
        }
    }
}
