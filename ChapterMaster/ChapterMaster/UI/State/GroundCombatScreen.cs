using ChapterMaster.Combat;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class GroundCombatScreen : Screen
    {
        public PrimitiveBuddy.Primitive Primitive;
        Planet planet;
        public List<Squad> Squads;
        public GroundCombatScreen(int screenId, string backgroundTexture, Align.Align align, Planet planet, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            this.planet = planet;
        }

        public override void Update(ViewController view)
        {
            base.Update(view);
        }

        private Point scalePoint(Point point, float factor) 
        {
            return new Point(point.X / factor, point.Y / factor); 
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            base.Render(spriteBatch, view); // TO DO: Change background to match planet type.
            string name = planet.GetName();
            spriteBatch.DrawString(Assets.ARJULIAN, name, new Vector2(view.viewPortWidth / 2 - Assets.ARJULIAN.MeasureString(name).X, 2), Color.White);
            for (int currentSquad = 0; currentSquad < Squads.Count; currentSquad++)
            {
                Squad squad = Squads[currentSquad];
                for (int i = 0; i < squad.Troops.Count; i++)
                {
                    Troop troop = squad.Troops[i];
                    Rectangle rect = new Rectangle(troop.Position.ToPoint(), troop.Size.ToPoint());
                    spriteBatch.Draw(Assets.UITextures["spr_mar_collision_0"],
                                     rect,Color.White);
                    spriteBatch.DrawString(Assets.ARJULIAN, $"{troop.Health}",
                                           new Vector2(troop.Position.X, troop.Position.Y - 10),
                                           Color.White);
                }
            }
        }

        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
        }
    }
}
