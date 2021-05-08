using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.World.Faction
{
    public class Faction
    {
        public bool IsPlayer;
        public string Name;
        public Color Color;
        public int FleetType;
        public string LeaderName;
        public Combat.Tree Force;
        public int HomeSystem = 0;
        public string HomeSystemName = "";
    }
}
