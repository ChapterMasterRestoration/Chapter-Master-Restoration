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
    public delegate void TextboxHandler(object sender, string value);
    public class TextBox : Button
    {
        protected TextboxHandler outOfFocus;
        protected TextboxHandler updateKey;
        public string Value;
        public int charLimit;
        public TextBox(string textboxTextureId, string initialText, MouseHandler onClick = null,
            TextboxHandler outOfFocus = null, TextboxHandler updateKey = null, int charLimit = 26) : base(
            textboxTextureId, initialText, onClick)
        {
            Value = initialText;
            this.charLimit = charLimit;
            this.outOfFocus = outOfFocus;
            this.updateKey = updateKey;
        }
        public TextBox(string textboxTextureId, string initialText, Align.Align align, MouseHandler onClick = null,
            TextboxHandler outOfFocus = null, TextboxHandler updateKey = null, int charLimit = 26) : base(
            textboxTextureId, initialText, onClick)
        {
            this.align = align;
            Value = initialText;
            this.charLimit = charLimit;
            this.outOfFocus = outOfFocus;
            this.updateKey = updateKey;
        }
        Keys currentKey;
        bool shift = false;
        public override void Check(ViewController view, Align.Align align)
        {
            int mouseX = view.GetMouse().X;
            int mouseY = view.GetMouse().Y;
            int ulCornerX = (int)position.X;
            int ulCornerY = (int)position.Y;
            int brCornerX = ulCornerX + align.width;
            int brCornerY = ulCornerY + align.height;

            if (view.GetMouse().LeftButton == ButtonState.Released) wasClicked = false;
            if (view.GetMouse().LeftButton == ButtonState.Pressed && !wasClicked)
            { 
               if (mouseX > ulCornerX && mouseY > ulCornerY && mouseX < brCornerX && mouseY < brCornerY)
               {
                   if(handler != null)
                        handler(view.GetMouse(), this);
                   inFocus = true;
                   wasClicked = true;
               }
               else
               {
                    inFocus = false;
                    if(outOfFocus != null)
                        outOfFocus(this, Value);
               }
               Debug.WriteLine("clicked " + mouseX + " br" + brCornerX);
            }
            var keys = Keyboard.GetState().GetPressedKeys();
            if (keys.Contains(Keys.LeftShift) || keys.Contains(Keys.Right)) shift = true;
            else shift = false;
            if (inFocus && Array.FindAll(keys, p => (p != Keys.LeftShift) && (p != Keys.RightShift)).Length > 0)
            {
                currentKey = Array.Find(keys, p => (p != Keys.LeftShift) && (p != Keys.RightShift));

            }
            if (inFocus && currentKey != Keys.None && Keyboard.GetState().IsKeyUp(currentKey)) 
            {
                var letter = currentKey.ToString();
                if (letter.Length > 1) letter = "";
                if (currentKey == Keys.Back)
                {
                    if (Value.Length > 0)
                        Value = Value.Remove(Value.Length - 1, 1);
                }
                else if (currentKey == Keys.Space)
                {
                    if(Value.Length < charLimit)
                        Value += " ";
                }
                else if(currentKey == Keys.Enter)
                {
                    inFocus = false; 
                }
                else
                {
                    if (Value.Length < charLimit)
                        Value += shift ? letter : letter.ToLower();
                }
                currentKey = Keys.None;
            }
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            position = new Vector2(align.GetRect(view).X, align.GetRect(view).Y); // TODO fix: don't even expose position
            if (buttonTextureId.Length > 0)
            {
                spriteBatch.Draw(Assets.ButtonTextures[buttonTextureId], align.GetRect(view), Color.White);
            }
            
            spriteBatch.DrawString(Assets.Courier_New, Value, position + new Vector2(25,10), Color.White);
        }
    }
}
