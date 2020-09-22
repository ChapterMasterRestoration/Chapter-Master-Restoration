﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    public class Force : Node
    {
        public bool grabbed = false;
        public string name;
        //public int number;
        public Vector2 position = new Vector2(0, 0);
        public int width = 125;
        public int height = 175;
        public Force(string name, int x, int y) : base()
        {
            this.name = name;
            this.position = new Vector2(x, y);
        }
    }
}