using ChapterMaster.Tree;
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
        public PrimitiveBuddy.Primitive primitive;
        Planet planet;
        public Squad squad;
        public GroundCombatScreen(int screenId, string backgroundTexture, Align.Align align, Planet planet, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            this.planet = planet;
            this.squad = new Squad(planet, new List<Troop>() {new Troop()});
        }

        public override void Update(ViewController view)
        {
            base.Update(view);
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            base.Render(spriteBatch, view); // TO DO: Change background to match planet type.
            string name = planet.GetName();
            spriteBatch.DrawString(Assets.ARJULIAN, name, new Vector2(view.viewPortWidth / 2 - Assets.ARJULIAN.MeasureString(name).X, 2), Color.White);
            for (int i = 0;  i < squad.Troops.Count; i++)
            {
                Troop troop = squad.Troops[i];
                spriteBatch.Draw(Assets.UITextures["spr_mar_collision_0"], troop.Position, Color.White);
                spriteBatch.DrawString(Assets.ARJULIAN, $"{troop.Health}", troop.Position, Color.White);
            }
        }

        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
        }
    }
}
