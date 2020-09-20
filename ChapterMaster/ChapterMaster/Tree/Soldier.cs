using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    public class Soldier : Node
    {
        public string name;
        public int number;
        public Soldier(string name, int number) : base()
        {
            this.name = name;
            this.number = number;
        }
    }
}
