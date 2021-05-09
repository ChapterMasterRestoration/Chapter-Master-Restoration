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

        bool dragging = false;
        Troop currentlySelectedTroop;
        Squad currentlySelectedSquad;
        Vector2 mouseOffset = new Vector2(0, 0);
        Vector2 troopOffset = new Vector2(0, 0);
        Vector2 orderStart = new Vector2(0, 0);
        Vector2 orderEnd = new Vector2(0, 0);
        bool assigningOrder = false;
        public override void Update(ViewController view)
        {
            base.Update(view);
            for (int currentSquad = 0; currentSquad < Squads.Count; currentSquad++)
            {
                Squad squad = Squads[currentSquad];
                for (int currentTroop = 0; currentTroop < squad.Troops.Count; currentTroop++)
                {
                    Troop troop = squad.Troops[currentTroop];
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        if (Mouse.GetState().RightButton == ButtonState.Pressed) // start drag
                        {
                            if (currentlySelectedTroop == null && troop.MouseOver(squad))
                            {
                                mouseOffset = Mouse.GetState().Position.ToVector2() - troop.Position;
                                troop.Grabbed = true;
                                currentlySelectedTroop = troop;
                                dragging = true;
                            }
                        } else
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed) // start order
                        {
                            if (currentlySelectedTroop == null && troop.MouseOver(squad))
                            {
                                mouseOffset = Mouse.GetState().Position.ToVector2() - troop.Position;
                                troop.Grabbed = true;
                                currentlySelectedTroop = troop;
                                dragging = false;
                                orderStart = squad.Position + troop.Position + new Vector2(troop.Size.X, troop.Size.Y / 2);
                                assigningOrder = true;
                            }
                        }
                        if (troop.Grabbed && currentlySelectedTroop == troop)
                        {
                            if (dragging)
                            {
                                Debug.WriteLine($"Currently moving troop i {currentTroop} to ${Mouse.GetState().Position.X}");
                                currentlySelectedTroop.Position = Mouse.GetState().Position.ToVector2() - mouseOffset;
                                if (Mouse.GetState().RightButton == ButtonState.Released) // release drag
                                {
                                    troop.Grabbed = false;
                                    currentlySelectedTroop = null;
                                    dragging = false;
                                }
                            } else
                            {
                                orderEnd = Mouse.GetState().Position.ToVector2();
                                if (Mouse.GetState().LeftButton == ButtonState.Released) // release order
                                {
                                    troop.Grabbed = false;
                                    currentlySelectedTroop = null;
                                    assigningOrder = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            if (currentlySelectedSquad == null && currentlySelectedTroop == null && troop.MouseOver(squad))
                            {
                                //mouseOffset = Mouse.GetState().Position.ToVector2() - troop.Position;
                                troopOffset = Mouse.GetState().Position.ToVector2() - squad.Position;
                                troop.Grabbed = true;
                                squad.Grabbed = true;
                                currentlySelectedTroop = troop;
                                currentlySelectedSquad = squad;
                            }
                        }
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {

                    }
                    if(troop.Grabbed && squad.Grabbed && currentlySelectedTroop == troop && currentlySelectedSquad == squad)
                    {
                        squad.Position = Mouse.GetState().Position.ToVector2() - troopOffset;
                        Debug.WriteLine($"Currently moving squad i {currentSquad} to ${Mouse.GetState().Position.X}");
                        if (Mouse.GetState().RightButton == ButtonState.Released)
                        {
                            troop.Grabbed = false;
                            squad.Grabbed = false;
                            currentlySelectedTroop = null;
                            currentlySelectedSquad = null;
                        }
                    }
                }
            }
        }
        Point ScalePoint(Point point, float factor)
        {
            return new Point((int)(point.X / factor), (int)(point.Y / factor));
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            base.Render(spriteBatch, view); // TODO Change background to match planet type.
            string name = planet.GetName();
            spriteBatch.DrawString(Assets.ARJULIAN, name, new Vector2(view.viewPortWidth / 2 - Assets.ARJULIAN.MeasureString(name).X, 2), Color.White);
            for (int currentSquad = 0; currentSquad < Squads.Count; currentSquad++)
            {
                Squad squad = Squads[currentSquad];
                for (int i = 0; i < squad.Troops.Count; i++)
                {
                    Troop troop = squad.Troops[i];
                    Rectangle rect = new Rectangle(squad.Position.ToPoint() + troop.Position.ToPoint(), troop.Size.ToPoint());
                    spriteBatch.Draw(Assets.UITextures["spr_mar_collision_0"],
                                     rect,Color.White);
                    string leaderTag = i == Squads.Count - 1 ? "L" : "";
                    spriteBatch.DrawString(Assets.ARJULIAN, $"{troop.Health} i {i}",
                                           squad.Position + new Vector2(troop.Position.X, troop.Position.Y - 10),
                                           Color.White);
                }
            }
            if(currentlySelectedTroop != null)
            {
                //spriteBatch.DrawString(Assets.ARJULIAN, $"cur: {currentlySelectedTroop}") // draw currently selected troop id
            }
            if(assigningOrder)
            {
                Vector2 position = orderStart + new Vector2(currentlySelectedTroop.Size.X + 5, currentlySelectedTroop.Size.Y / 2);
                Rectangle rect = new Rectangle(position.ToPoint().X, 
                    position.ToPoint().Y, 
                    Math.Abs((orderEnd - position).ToPoint().X), 
                    Math.Abs((orderEnd - position).ToPoint().Y));
                float rot = (float)Math.Atan2(orderEnd.Y - orderStart.Y, orderEnd.X - orderStart.X);
                float scale = (orderEnd - orderStart).Length()/128;
                spriteBatch.Draw(Assets.UITextures["order_move_arrow"], position, null, 
                    Color.White, rot, 
                    new Vector2(0, currentlySelectedTroop.Size.Y/2 + 16),
                    new Vector2(scale, 1 + scale/20),
                    SpriteEffects.None, 0);

                Debug.WriteLine($"PX: {position.X}, PY: {position.Y}, RX: {rect.X}, RY: {rect.Y}, RW: {rect.Width}, RH: {rect.Height}, RO: {rot}");
            }
        }

        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
        }
    }
}
