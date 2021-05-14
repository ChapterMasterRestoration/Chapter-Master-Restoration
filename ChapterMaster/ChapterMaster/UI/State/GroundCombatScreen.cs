using ChapterMaster.Combat;
using ChapterMaster.World;
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
    public class GroundCombatScreen : Screen
    {
        public PrimitiveBuddy.Primitive Primitive;
        Planet planet;
        public List<Squad> Squads;
        public GroundCombatScreen(int screenId, string backgroundTexture, Align.Align align, Planet planet, bool DoesOcclusion = true) : base(screenId, backgroundTexture, align, DoesOcclusion)
        {
            this.planet = planet;
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            base.Render(spriteBatch, view); // TODO Change background to match planet type.
            CombatViewController combatView = (CombatViewController) view;
            string name = planet.GetName();
            Vector2 nameSize= Assets.ARJULIAN.MeasureString(name);
            spriteBatch.DrawString(Assets.ARJULIAN, name, new Vector2(view.viewPortWidth / 2 - nameSize.X / 2, 2), Color.White);
            if (combatView.IsDeploying())
            {
                string deploymentPhase = "Deployment Phase (Press shift to give initial move orders and place individual troops)";
                Vector2 deploymentSize = Assets.ARJULIAN.MeasureString(deploymentPhase);
                spriteBatch.DrawString(Assets.ARJULIAN, deploymentPhase, new Vector2(view.viewPortWidth / 2 - deploymentSize.X / 2, nameSize.Y + 2),
                    Color.White);
            }

            for (int currentSquad = 0; currentSquad < Squads.Count; currentSquad++)
            {
                Squad squad = Squads[currentSquad];
                for (int i = 0; i < squad.Troops.Count; i++)
                {
                    Troop troop = squad.Troops[i];
                    troop.Draw(spriteBatch, combatView, squad); 
                    //string leaderTag = i == Squads.Count - 1 ? "L" : "";
                    //spriteBatch.DrawString(Assets.ARJULIAN, $"{troop.Health} i {i}",
                    //                       squad.Position + new Vector2(troop.Position.X, troop.Position.Y - 10) - combatView.GetCameraPosition(),
                    //                       Color.White);
                }
            }
            if(combatView.currentlySelectedTroop != null)
            {
                //spriteBatch.DrawString(Assets.ARJULIAN, $"cur: {currentlySelectedTroop}") // draw currently selected troop id
            }
            if(combatView.assigningOrder)
            {
                
                /*Rectangle rect = new Rectangle(position.ToPoint().X, 
                    position.ToPoint().Y, 
                    Math.Abs((orderEnd - position).ToPoint().X), 
                    Math.Abs((orderEnd - position).ToPoint().Y));*/
                Debug.WriteLine("drawing order arrow");
                spriteBatch.Draw(Assets.UITextures["order_move_arrow"], combatView.GetStartPositionOfOrder() - combatView.GetCameraPosition(), null, 
                    Color.White, combatView.GetOrderRotation(), 
                    new Vector2(0,  16),
                    new Vector2(combatView.GetOrderScale(), 1 + combatView.GetOrderScale()/20),
                    SpriteEffects.None, 0);

                //Debug.WriteLine($"PX: {position.X}, PY: {position.Y}, RX: {rect.X}, RY: {rect.Y}, RW: {rect.Width}, RH: {rect.Height}, RO: {rot}");
            }
        }

        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
        }
    }
}
