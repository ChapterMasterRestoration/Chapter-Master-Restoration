using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    class Constants
    {
        public const string ImageDirectory = "textures";
        public const int SystemSize = 80;
        public const int WorldWidth = 1024;
        public const int WorldHeight = 1024;
        /*
        Chaos = 0
        Civilian = 1
        Eldar = 2
        Imperial = 3
        Necron = 4
        Tau = 5
        Tiny = 6
        Tyranid = 7
        Ork = 8
        Mechanicus = 9
        Inquisition = 10
        */
        public static readonly string[] FleetTexture = { "chaos", "civilian", "eldar", "imperial", "necron", "tau", "tiny", "tyranid", "ork", "mechanicus", "inquisition" };
        public static readonly int[] FleetStateLimit = { 10, 4, 6, 10, 9, 11, 10, 12, 10, 1, 3 };
    }
}
