using ChapterMaster.Tree;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public delegate int CalculateWidth(Soldier node); 
    public class ForceOrganizerScreen : Screen
    {
        public PrimitiveBuddy.Primitive primitive;
        public ForceOrganizerScreen(int screenId, string backgroundTexture, Align.Align align, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {

        }

        public void Update(ViewController view, Tree.Tree tree)
        {
            base.Update(view);
        }
        public int CalculateWidth(Soldier node)
        {
            return (int)Assets.ARJULIAN.MeasureString($"Soldier: {node.name} {node.number}").X;
        }
        public void Render(SpriteBatch spriteBatch, ViewController view, Tree.Tree tree)
        {
            base.Render(spriteBatch, view);
            Vector2 left = new Vector2(50, 50);
            Vector2 top = new Vector2(Rect.Width / 2, 50);
            string topText = $"Top: {tree.Parent.name} {tree.Parent.number}";
            Vector2 sizeTextTop = Assets.ARJULIAN.MeasureString(topText);
            int layer = 0;
            spriteBatch.DrawString(Assets.ARJULIAN, topText, top, Color.White);
            // how to implement depth? maybe not recursion for now. only 4 levels is bound to be enough.
            layer = 1;
            left.Y += layer * 50;
            Vector2 level1Left = left;
            // all this crap is temporary till I get proper a proper 
            // recursive method to calculate width on generic trees
            for (int currentSoldier = 0; currentSoldier < tree.Parent.GetNumberOfChildren(); currentSoldier++)
            {
                Soldier soldier = (Soldier)tree.Parent.GetChildren()[currentSoldier];
                string text = $"Soldier: {soldier.name} {soldier.number}";
                Vector2 size1Text = Assets.ARJULIAN.MeasureString(text);
                level1Left.X += currentSoldier * size1Text.X + 20; // account for deeper layers
                spriteBatch.DrawString(Assets.ARJULIAN, text, level1Left, Color.White);
                primitive.Line(top + new Vector2(sizeTextTop.X / 2, sizeTextTop.Y + 5), level1Left + new Vector2(size1Text.X/2,0), Color.White);
                Vector2 level2Left = level1Left;
                level2Left.Y += 50;
                for(int thirdLevel = 0; thirdLevel < soldier.GetNumberOfChildren(); thirdLevel++)
                {
                    Soldier thirdLevelSoldier = (Soldier)soldier.GetChildren()[thirdLevel];
                    text = $"Soldier: {thirdLevelSoldier.name} {thirdLevelSoldier.number}";
                    Vector2 size2Text = Assets.ARJULIAN.MeasureString(text);
                    level2Left.X += thirdLevel * size2Text.X + 20;
                    spriteBatch.DrawString(Assets.ARJULIAN, text, level2Left, Color.White);
                    primitive.Line(level1Left + new Vector2(size1Text.X / 2, size1Text.Y + 5), level2Left + new Vector2(size2Text.X / 2, 0), Color.White);
                }
            }
        }
    }
}
