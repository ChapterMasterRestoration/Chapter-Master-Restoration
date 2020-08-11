using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    class Constants
    {
        public const string IMAGE_DIRECTORY = "textures";
        public const int SYSTEM_WIDTH_HEIGHT = 80;
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
        public static readonly string[] FLEET_TEXTURE_ID_FILE = { "chaos", "civilian", "eldar", "imperial", "necron", "tau", "tiny", "tyranid", "ork", "mechanicus", "inquisition" };
        public static readonly int[] FLEET_STATE_LIMIT = { 10, 4, 6, 10, 9, 11, 10, 12, 10, 1, 3 };
    }
}
