using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using Vector2Extensions;

namespace ChapterMaster.Combat
{
    public class Troop
    {
        public int Texture = -1;
        public float Health = 200;
        public float Armour = 150;
        public float MoveSpeed = 30;
        private bool isMoving = false;

        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Size = new Vector2(40, 40);
        public float Rotation = 0.0f;
        
        public Weapon.Weapon Weapon;
        public Troop Target;
        public bool Grabbed;
        //public bool Selected = false;

        public Troop()
        {

        }

        public void Draw(SpriteBatch spriteBatch, CombatViewController view, Squad squad)
        {
            Rectangle rect = new Rectangle(squad.Position.ToPoint() + this.Position.ToPoint() - view.GetCameraPosition().ToPoint(), this.Size.ToPoint());
            if (squad.Selected)
            {
                spriteBatch.DrawRectangle(GetRectangle(view, squad), Color.Yellow);
            }

            if (squad.IsSquadLeader(this))
            {
                spriteBatch.DrawString(Assets.Courier_New, "" + squad.GetHealth(), 
                    rect.Location.ToVector2() -(Size / 2 + new Vector2(0, + 4)), Color.White);
                spriteBatch.Draw(Assets.UITextures["gc_squadleader"], rect.Location.ToVector2(), null,
                    Color.White, this.Rotation,
                    new Vector2(Size.X / 2, Size.Y / 2),
                    new Vector2(1, 1),
                    SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(Assets.UITextures["gc_spacemarine"], rect.Location.ToVector2() - Size/4, null,
                    Color.White, this.Rotation,
                    new Vector2(Size.X / 2, Size.Y / 2),
                    new Vector2(1, 1),
                    SpriteEffects.None, 0);
            }

        }

        public Rectangle GetRectangle(CombatViewController view, Squad squad)
        {
            return new Rectangle((Position + squad.Position - view.GetCameraPosition()).ToPoint() -  Size.ToPoint().Multiply(0.5f), Size.ToPoint());
        }

        public bool MouseOver(CombatViewController view, Squad squad) // Will probably have to be moved to ViewController.S
        {
            return GetRectangle(view, squad).Contains(Mouse.GetState().Position);
        }

        public bool IsCollidingWith(CombatViewController view, Squad thisSquad, Squad otherSquad,Troop otherTroop)
        {
            return GetRectangle(view, thisSquad).Intersects(otherTroop.GetRectangle(view, otherSquad));
        }

        public bool IsCollidingWithAny(CombatViewController view, Squad thisSquad, Squad otherSquad)
        {
            foreach(Troop otherTroop in otherSquad.Troops)
            {
                if (this != otherTroop)
                {
                    if (GetRectangle(view, thisSquad).Intersects(otherTroop.GetRectangle(view, otherSquad)))
                        return true;
                }
            }
            //Debug.WriteLine("checking for collision");
            return false;
        }
     }
}
