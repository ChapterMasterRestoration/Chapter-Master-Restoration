using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    public class Soldier : Node
    {
        public bool grabbed = false;
        public string name;
        public int number;
        public Vector2 position = new Vector2(0, 0);
        public Soldier(string name, int number) : base()
        {
            this.name = name;
            this.number = number;
        }
    }
}
