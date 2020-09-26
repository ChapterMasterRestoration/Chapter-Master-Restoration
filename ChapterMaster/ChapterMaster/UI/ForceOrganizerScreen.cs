using ChapterMaster.Tree;
using ChapterMaster.Util;
using Humper;
using Humper.Responses;
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
        public ViewController view;
        private SpriteBatch _spriteBatch;
        public PrimitiveBuddy.Primitive primitive;
        public ForceOrganizerScreen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {

        }
        private ButtonState previousMouseState;
        private Vector2 previousMousePosition = new Vector2(0, 0);
        Force currentlySelectedForce;
        Vector2 mouseOffset = new Vector2(0, 0);
        Vector2 resizeSize = new Vector2(0, 0);
        bool isResizing = false;
        bool collided = false;
        private Tree.Tree tree;
        Humper.World world;
        private void UpdateForce(Node node)
        {
            Force force = (Force)node;
            world = new Humper.World(view.viewPortWidth, view.viewPortHeight);
            string text =  $"{force.name}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            Rectangle box = new Rectangle(new Point((int) force.position.X, (int) force.position.Y), new Point(force.width, force.height));
            Rectangle resizeBox = new Rectangle(new Point(
                                                         (int)force.position.X + force.width - 18, 
                                                         (int)force.position.Y + force.height - 17), 
                                                new Point(18, 17));
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (resizeBox.Contains(Mouse.GetState().Position))
                {
                    isResizing = true;
                    currentlySelectedForce = force;

                    Debug.WriteLine($"Resize X: {resizeSize.X}, Resize Y: {resizeSize.Y}");
                }

                if(isResizing && currentlySelectedForce == force)
                {
                    Vector2 sizeCurrentText = Assets.ARJULIAN.MeasureString(currentlySelectedForce.name);
                    resizeSize = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - currentlySelectedForce.position;
                    currentlySelectedForce.width = Math.Max((int)sizeCurrentText.X, (int)resizeSize.X);
                    currentlySelectedForce.height = (int)resizeSize.Y;
                    box = new Rectangle(new Point((int)force.position.X, (int)force.position.Y), new Point(force.width, force.height));
                }

                if (currentlySelectedForce == null && box.Contains(Mouse.GetState().Position) && !isResizing)
                {
                    mouseOffset = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - force.position;
                    force.grabbed = true;
                    currentlySelectedForce = force;

                    Debug.WriteLine($"X: {force.position.X}, Y: {force.position.Y}, Force Name: {force.name}");
                }
            }

            IBox current = world.Create(force.position.X, force.position.Y, force.width, force.height); // ObscuredCode apologizes to those who suffer cataracts from reading this.
            if (currentlySelectedForce != null)
            {
                current = world.Create(currentlySelectedForce.position.X, currentlySelectedForce.position.Y, currentlySelectedForce.width, currentlySelectedForce.height);
            }
            Vector2 offset;
            void DoCollisionTest(Node node1)
            {
                Force force1 = (Force)node1;
                IBox other = world.Create(force1.position.X, force1.position.Y, force1.width, force1.height);
                //if (currentlySelectedForce != force1 && currentlySelectedForce.GetRectangle().Intersects(new Rectangle(new Point((int)force1.position.X, (int)force1.position.Y), new Point(force1.width, force1.height))))
                //{
                //    //collided = true;
                //    offset = currentlySelectedForce.position - force1.position;
                //    currentlySelectedForce.position += offset;

                //    Debug.WriteLine($"Current: {currentlySelectedForce.name}, {force1.name}, oX: {offset.X}, oY: {offset.Y}");
                //}
            }

            if (currentlySelectedForce == force)
            {
                tree.Parent.Traverse(tree.Parent, DoCollisionTest);
            }

            if (force.grabbed && currentlySelectedForce == force && !isResizing && currentlySelectedForce.MouseOver() && !collided)
            {
                //new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - box.Location.ToVector2();
                force.position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - mouseOffset; // - mouseOffset/2 makes it really smooth;
                current.Move(force.position.X, force.position.Y, (collision) => CollisionResponses.Slide);
                force.position = new Vector2(current.X, current.Y);
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    force.grabbed = false;
                    currentlySelectedForce = null; // Try commenting this.
                }
            }

            bool justCollided = false;
            void DidCollide(Node node1)
            {
                Force force1 = (Force)node1;
                if (currentlySelectedForce != force1 && currentlySelectedForce.GetRectangle().Intersects(new Rectangle(new Point((int)force1.position.X, (int)force1.position.Y), new Point(force1.width, force1.height))))
                {
                    justCollided = true;
                }
            }

            if (currentlySelectedForce == force)
            {
                tree.Parent.Traverse(tree.Parent, DidCollide);
            }

            if (isResizing && Mouse.GetState().LeftButton == ButtonState.Released && currentlySelectedForce != null && !justCollided)
            {
                Vector2 sizeCurrentText = Assets.ARJULIAN.MeasureString(currentlySelectedForce.name);
                isResizing = false;
                resizeSize = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - currentlySelectedForce.position;
                currentlySelectedForce.width = Math.Max((int)sizeCurrentText.X, (int)resizeSize.X);
                currentlySelectedForce.height = (int)resizeSize.Y;
                currentlySelectedForce = null;
                box = new Rectangle(box.Location, resizeSize.ToPoint());

                Debug.WriteLine("The wealth of those societies in which the capitalist mode of production prevails sucks");
            }

            previousMouseState = Mouse.GetState().LeftButton;
            previousMousePosition = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
        }

        public void Update(ViewController view, Tree.Tree tree)
        {
            base.Update(view);
            this.tree = tree;
            this.view = view; // TO DO: Fix this.
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
            Vector2 posText = new Vector2(0, 2);
            posText.X = force.width / 2 - sizeText.X / 2;
            _spriteBatch.Draw(Assets.UITextures["force_background"], new Rectangle(force.position.ToPoint(),new Point(force.width, force.height)), Color.White);
            _spriteBatch.DrawString(Assets.ARJULIAN, text, force.position + posText, Color.White);
            if (force.Parent != null)
            {
                Force parent = (Force)force.Parent;
                string textParent = $"{parent.name}";
                Vector2 sizeParentText = Assets.ARJULIAN.MeasureString(textParent);
                primitive.Line(parent.position + new Vector2(((Force)force.Parent).width / 2, ((Force)force.Parent).height), force.position + new Vector2(force.width / 2, 0), Color.White);
            }

            Rectangle box = new Rectangle(new Point((int)force.position.X, (int)force.position.Y), new Point(force.width, force.height));
            if (force.grabbed)
            {

            }
            //if (box.Contains(Mouse.GetState().Position))
            //{
            //    primitive.Line(force.position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);
            //}
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
            if (isResizing)
            {
                spriteBatch.DrawString(Assets.ARJULIAN, $"Is resizing", new Vector2(0, 5), Color.White);
            }
        }
    }
}
