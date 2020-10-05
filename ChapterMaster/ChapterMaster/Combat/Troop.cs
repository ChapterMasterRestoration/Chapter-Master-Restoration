using Microsoft.Xna.Framework;
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

        public Troop()
        {

        }
    }
}
