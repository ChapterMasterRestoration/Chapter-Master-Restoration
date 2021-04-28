using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Vector2 Size = new Vector2(31, 39);

        public Weapon.Weapon Weapon;
        public Troop Target;
        public bool Grabbed;

        public Troop()
        {

        }

        public Rectangle GetRectangle(Squad squad)
        {
            return new Rectangle((Position + squad.Position).ToPoint(), Size.ToPoint());
        }

        public bool MouseOver(Squad squad) // Will probably have to be moved to ViewController.S
        {
            return GetRectangle(squad).Contains(Mouse.GetState().Position);
        }
     }
}
