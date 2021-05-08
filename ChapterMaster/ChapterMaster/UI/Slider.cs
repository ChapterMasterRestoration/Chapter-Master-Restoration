using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public delegate void SliderHandler(object sender, int value);
    public class Slider : Button
    {
        protected SliderHandler outOfFocus;
        protected SliderHandler updateValue;
        public int Value;
        public int minValue;
        public int maxValue;
        public float increment;
        protected Slider(string sliderTextureId, int initialValue, int minValue, int maxValue, float increment = 1.0f,
            MouseHandler onClick = null, SliderHandler outOfFocus = null, SliderHandler updateValue = null) : base(
            sliderTextureId, "", onClick)
        {
            Value = initialValue;
            this.outOfFocus = outOfFocus;
            this.updateValue = updateValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.increment = increment;
        }

        public Slider(string sliderTextureId, int initialValue, Align.Align align, int minValue, int maxValue,
            float increment = 1.0f, MouseHandler onClick = null, SliderHandler outOfFocus = null,
            SliderHandler updateValue = null) : base(sliderTextureId, "", align, onClick)
        {
            this.align = align;
            Value = initialValue;
            this.outOfFocus = outOfFocus;
            this.updateValue = updateValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.increment = increment;
        }

        public override void Check(ViewController view, Align.Align align)
        {
            int mouseX = view.GetMouse().X;
            int mouseY = view.GetMouse().Y;
            int ulCornerX = (int) position.X;
            int ulCornerY = (int) position.Y;
            int brCornerX = ulCornerX + align.width;
            int brCornerY = ulCornerY + align.height;

            if (view.GetMouse().LeftButton == ButtonState.Released) wasClicked = false;
            if (view.GetMouse().LeftButton == ButtonState.Pressed && !wasClicked)
            {
                if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
                {
                    if (handler != null)
                        handler(view.GetMouse(), this);
                    inFocus = true;
                    wasClicked = true;
                }
                else
                {
                    inFocus = false;
                    if (outOfFocus != null)
                        outOfFocus(this, Value);
                }

                Debug.WriteLine("clicked " + mouseX + " br" + brCornerX);
            }
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            position = new Vector2(align.GetRect(view).X, align.GetRect(view).Y); // TO DO: fix: don't even expose position
            if (buttonTextureId.Length > 0)
            {
                spriteBatch.Draw(Assets.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
            }
            
            //spriteBatch.DrawString(Assets.Courier_New, Value, position + new Vector2(25,10), Color.White);
        }
    }
}