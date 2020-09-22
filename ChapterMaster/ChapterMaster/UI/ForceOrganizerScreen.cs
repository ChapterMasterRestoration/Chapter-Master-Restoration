using ChapterMaster.Tree;
using ChapterMaster.Util;
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
    public delegate int CalculateWidth(Force node); 
    public class ForceOrganizerScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        public PrimitiveBuddy.Primitive primitive;
        public ForceOrganizerScreen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {

        }
        private ButtonState previousMouseState;
        private Vector2 previousMousePosition = new Vector2(0, 0);
        Force currentlySelectedForce;
        private void UpdateForce(Node node)
        {
            Force force = (Force)node;
            string text =  $"{force.name}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            Rectangle box = new Rectangle(new Point((int) force.position.X, (int) force.position.Y), new Point(force.width, force.height));
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && currentlySelectedForce == null)      
            {
                if (box.Contains(Mouse.GetState().Position))
                {
                    force.grabbed = true;
                    currentlySelectedForce = force;
                    Debug.WriteLine($"X: {force.position.X}, Y: {force.position.Y}, Force Name: {force.name}");
                }
            }
            
            if (force.grabbed && currentlySelectedForce != null)
            {
                Vector2 mouseOffset = sizeText; //new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - box.Location.ToVector2();
                force.position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y); // - mouseOffset/2; //TO DO: Make selected element not move when selected, this works for now.
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    force.grabbed = false;
                    currentlySelectedForce = null;
                }
            }
            previousMouseState = Mouse.GetState().LeftButton;
            previousMousePosition = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
        }

        public void Update(ViewController view, Tree.Tree tree)
        {
            base.Update(view);
            tree.Parent.Traverse(tree.Parent, UpdateForce);
        }
        public int CalculateWidth(Force node)
        {
            return (int)Assets.ARJULIAN.MeasureString($"{node.name}").X;
        }
        private void RenderForce(Node node)
        {
            Force force = (Force)node;
            string text = $"{force.name}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            _spriteBatch.Draw(Assets.UITextures["force_background"], new Rectangle(force.position.ToPoint(),new Point(force.width, force.height)), Color.White);
            _spriteBatch.DrawString(Assets.ARJULIAN, text, force.position, Color.White);
            if (force.Parent != null)
            {
                Force parent = (Force)force.Parent;
                string textParent = $"{parent.name}";
                Vector2 sizeParentText = Assets.ARJULIAN.MeasureString(textParent);
                primitive.Line(parent.position + new Vector2(force.width / 2, force.height), force.position + new Vector2(force.width / 2, 0), Color.White);
            }
        }
        public void Render(SpriteBatch spriteBatch, ViewController view, Tree.Tree tree)
        {
            _spriteBatch = spriteBatch;
            base.Render(spriteBatch, view);
            tree.Parent.Traverse(tree.Parent, RenderForce);
            if (currentlySelectedForce != null)
            {
                spriteBatch.DrawString(Assets.ARJULIAN, $"{currentlySelectedForce.name}", new Vector2(0, 5), Color.White);
            }
        }
    }
}
