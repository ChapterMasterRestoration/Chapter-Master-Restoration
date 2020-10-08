using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Combat
{
    public class Force : Node
    {
        public bool Grabbed = false;
        public string Name;
        //public int number;
        public Vector2 Position = new Vector2(0, 0);
        public int Width = 125;
        public int Height = 175;
        public List<Squad> Squads = new List<Squad>();
        public Force(string name, int x, int y) : base()
        {
            this.Name = name;
            this.Position = new Vector2(x, y);
        }
        public bool MouseOver()
        {
            return new Rectangle(Position.ToPoint(), new Point(Width, Height)).Contains(Mouse.GetState().Position);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle (Position.ToPoint(), new Point(this.Width, this.Height));
        }
    }
}
