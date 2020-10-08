using ChapterMaster.State;
using ChapterMaster.Combat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace ChapterMaster.UI
{
    public delegate int CalculateWidth(Force node);
    public class ForceOrganizerScreen : Screen
    {
        private ViewController view;
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
        private Combat.Tree tree;
        private void UpdateForce(Node node)
        {
            Force force = (Force)node;
            string text = $"{force.Name}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            Rectangle resizeBox = new Rectangle(new Point(
                                                         (int)force.Position.X + force.Width - 18,
                                                         (int)force.Position.Y + force.Height - 17),
                                                new Point(18, 17));
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (resizeBox.Contains(Mouse.GetState().Position))
                {
                    isResizing = true;
                    currentlySelectedForce = force;

                    Debug.WriteLine($"Resize X: {resizeSize.X}, Resize Y: {resizeSize.Y}");
                }

                if (isResizing && currentlySelectedForce == force)
                {
                    Vector2 sizeCurrentText = Assets.ARJULIAN.MeasureString(currentlySelectedForce.Name);
                    resizeSize = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - currentlySelectedForce.Position;
                    currentlySelectedForce.Width = Math.Max((int)sizeCurrentText.X, (int)resizeSize.X);
                    currentlySelectedForce.Height = (int)resizeSize.Y;
                }

                if (currentlySelectedForce == null && force.MouseOver() && !isResizing)
                {
                    mouseOffset = Mouse.GetState().Position.ToVector2() - force.Position;
                    force.Grabbed = true;
                    currentlySelectedForce = force;

                    Debug.WriteLine($"X: {force.Position.X}, Y: {force.Position.Y}, Force Name: {force.Name}");
                }
            }
            else
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed && force.GetRectangle().Contains(Mouse.GetState().Position))
                {
                    Program.GameManager.ChangeState(new ForceManagerState(Program.GameManager, Program.GameManager.GraphicsDevice, Program.GameManager.Content, force));
                    return;
                }
            }

            if (force.Grabbed && currentlySelectedForce == force && !isResizing && !collided)
            {
                //new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - box.Location.ToVector2();
                force.Position = Mouse.GetState().Position.ToVector2() - mouseOffset; // - mouseOffset/2 makes it really smooth;
                if (force.Position.X < 0) force.Position.X = 0;
                if (force.Position.Y < 0) force.Position.Y = 0;
                if (force.GetRectangle().Right > view.viewPortWidth) force.Position.X = view.viewPortWidth - force.Width;
                if (force.GetRectangle().Bottom > view.viewPortHeight) force.Position.Y = view.viewPortHeight - force.Height;
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    force.Grabbed = false;
                    currentlySelectedForce = null; // Try commenting this.
                }
            }

            bool justCollided = false;
            void DidCollide(Node node1)
            {
                Force force1 = (Force)node1;
                if (currentlySelectedForce != force1 && currentlySelectedForce.GetRectangle().Intersects(new Rectangle(new Point((int)force1.Position.X, (int)force1.Position.Y), new Point(force1.Width, force1.Height))))
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
                Vector2 sizeCurrentText = Assets.ARJULIAN.MeasureString(currentlySelectedForce.Name);
                isResizing = false;
                resizeSize = Mouse.GetState().Position.ToVector2() - currentlySelectedForce.Position;
                currentlySelectedForce.Width = Math.Max((int)sizeCurrentText.X, (int)resizeSize.X);
                currentlySelectedForce.Height = (int)resizeSize.Y;
                currentlySelectedForce = null;

                //Debug.WriteLine("It is the 41st Millennium. For more than a hundred centuries The Emperor has sat immobile on the Golden Throne of Earth. He is the Master of Mankind by the will of the gods, and master of a million worlds by the might of his inexhaustible armies. He is a rotting carcass writhing invisibly with power from the Dark Age of Technology. He is the Carrion Lord of the Imperium for whom a thousand souls are sacrificed every day, so that he may never truly die.");
            }

            Vector2 offset;
            void DoCollisionTest(Node node1)
            {
                Force force1 = (Force)node1;
                if (currentlySelectedForce != force1 && currentlySelectedForce.GetRectangle().Intersects(force1.GetRectangle()))
                {
                    //collided = true;
                    offset = currentlySelectedForce.Position - force1.Position;
                    currentlySelectedForce.Position += offset / ((float)1);
                    if (force1.GetChildren().Contains(currentlySelectedForce) && currentlySelectedForce.Parent != null)
                    {
                        //force1.GetChildren().Remove(currentlySelectedForce);
                        //for (int kid = 0; kid < force1.GetNumberOfChildren(); kid++)          
                        //    force1.GetChildren()[kid].Emancipated = true;
                        currentlySelectedForce.Emancipated = true;
                        currentlySelectedForce.Parent = null;
                    }
                    else
                    {
                        if (force1.Parent != currentlySelectedForce) { // TODO: parent somewhere is stil null, fix that.
                            if (currentlySelectedForce.Parent != null)
                            {
                                Force copy = new Force(currentlySelectedForce.Name, (int)currentlySelectedForce.Position.X, (int)currentlySelectedForce.Position.Y);
                                for (int i = 0; i < currentlySelectedForce.GetNumberOfChildren(); i++)
                                {
                                    copy.AddChild(currentlySelectedForce.GetChildren()[i]);
                                }
                                copy.Width = currentlySelectedForce.Width;
                                copy.Height = currentlySelectedForce.Height;
                                copy.Name = currentlySelectedForce.Name;
                                copy.Parent = null;
                                currentlySelectedForce.Parent.GetChildren().Remove(currentlySelectedForce);
                                currentlySelectedForce = copy;
                            }

                            currentlySelectedForce.Emancipated = false;
                            force1.AddChild(currentlySelectedForce);
                        }
                    }

                    Debug.WriteLine($"Current: {currentlySelectedForce.Name}, {force1.Name}, oX: {offset.X}, oY: {offset.Y}");
                }
            }

            if (currentlySelectedForce == force)
            {
                tree.Parent.Traverse(tree.Parent, DoCollisionTest);
            }

            previousMouseState = Mouse.GetState().LeftButton;
            previousMousePosition = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
        }

        public void Update(ViewController view, Combat.Tree tree)
        {
            base.Update(view);
            this.tree = tree;
            this.view = view;
            tree.Parent.Traverse(tree.Parent, UpdateForce);
        }
        public int CalculateWidth(Force node)
        {
            return (int)Assets.ARJULIAN.MeasureString($"{node.Name}").X;
        }
        private void RenderForce(Node node)
        {
            Force force = (Force)node;
            string text = $"{force.Name}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            Vector2 posText = new Vector2(0, 2);
            posText.X = force.Width / 2 - sizeText.X / 2;

            _spriteBatch.Draw(Assets.UITextures["force_background"], new Rectangle(force.Position.ToPoint(), new Point(force.Width, force.Height)), Color.White);
            _spriteBatch.DrawString(Assets.ARJULIAN, text, force.Position + posText, Color.White);
            if (force.Parent != null && !force.Emancipated)
            {
                Force parent = (Force)force.Parent;
                string textParent = $"{parent.Name}";
                Vector2 sizeParentText = Assets.ARJULIAN.MeasureString(textParent);
                primitive.Line(parent.Position + new Vector2(((Force)force.Parent).Width / 2, ((Force)force.Parent).Height), force.Position + new Vector2(force.Width / 2, 0), Color.White);
            }

            Rectangle box = new Rectangle(new Point((int)force.Position.X, (int)force.Position.Y), new Point(force.Width, force.Height));
            if (force.Grabbed)
            {

            }
            //if (box.Contains(Mouse.GetState().Position))
            //{
            //    primitive.Line(force.position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);
            //}
        }

        public void Render(SpriteBatch spriteBatch, ViewController view, Combat.Tree tree)
        {
            _spriteBatch = spriteBatch;
            base.Render(spriteBatch, view);
            tree.Parent.Traverse(tree.Parent, RenderForce);
            if (currentlySelectedForce != null)
            {
                spriteBatch.DrawString(Assets.ARJULIAN, $"{currentlySelectedForce.Name}", new Vector2(0, 5), Color.White);
            }
            if (isResizing)
            {
                spriteBatch.DrawString(Assets.ARJULIAN, $"Is resizing", new Vector2(0, 5), Color.White);
            }
        }
    }
}
