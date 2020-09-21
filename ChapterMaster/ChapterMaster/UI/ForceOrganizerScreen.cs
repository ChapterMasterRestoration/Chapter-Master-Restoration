using ChapterMaster.Tree;
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
    public delegate int CalculateWidth(Soldier node); 
    public class ForceOrganizerScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        public PrimitiveBuddy.Primitive primitive;
        public ForceOrganizerScreen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {

        }
        private ButtonState previousMouseState;
        private Vector2 previousMousePosition = new Vector2(0, 0);
        private void UpdateSoldier(Node node)
        {
            Soldier soldier = (Soldier)node;
            string text = $"Soldier: {soldier.name} {soldier.number}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            Rectangle box = new Rectangle(new Point((int) soldier.position.X, (int) soldier.position.Y), new Point((int) sizeText.X, (int) sizeText.Y));
            if (box.Contains(Mouse.GetState().Position))      
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    soldier.grabbed = true;
                    Debug.WriteLine($"X: {soldier.position.X}, Y: {soldier.position.Y}");
                }
            }
            
            if (soldier.grabbed)
            {
                soldier.position += new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) - previousMousePosition;
                if (previousMouseState == ButtonState.Released)
                {
                    soldier.grabbed = false;
                }
            }
            previousMouseState = Mouse.GetState().LeftButton;
            
        }

        public void Update(ViewController view, Tree.Tree tree)
        {
            base.Update(view);
            tree.Parent.Traverse(tree.Parent, UpdateSoldier);
        }
        public int CalculateWidth(Soldier node)
        {
            return (int)Assets.ARJULIAN.MeasureString($"Soldier: {node.name} {node.number}").X;
        }
        private void RenderSoldier(Node node)
        {
            Soldier soldier = (Soldier)node;
            string text = $"Soldier: {soldier.name} {soldier.number}";
            Vector2 sizeText = Assets.ARJULIAN.MeasureString(text);
            _spriteBatch.DrawString(Assets.ARJULIAN, text, soldier.position, Color.White);
            if (soldier.Parent != null)
            {
                Soldier parent = (Soldier)soldier.Parent;
                string textParent = $"Soldier: {parent.name} {parent.number}";
                Vector2 sizeParentText = Assets.ARJULIAN.MeasureString(textParent);
                primitive.Line(parent.position + new Vector2(sizeParentText.X / 2, sizeParentText.Y + 5), soldier.position + new Vector2(sizeText.X / 2, 0), Color.White);
            }
        }
        public void Render(SpriteBatch spriteBatch, ViewController view, Tree.Tree tree)
        {
            _spriteBatch = spriteBatch;
            base.Render(spriteBatch, view);
            tree.Parent.Traverse(tree.Parent, RenderSoldier);
        }
    }
}
