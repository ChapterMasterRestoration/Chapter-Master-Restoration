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
        public static readonly string[] PlanetNames = new string[] { "I", "II", "III", "IV", "V" };
        public static readonly string[] PlanetTypeNames = new string[] { "Lava", "Temperate", "Desert", "Forge", "Hive", "Death", "Agri", "Necron", "Feudal", "Desert2", "Ice", "Water", "Dead", "Desert3", "Daemon", "Shrine", "Spacehulk" };
        public static int[] PlanetTypeTextures = { 1, 9, 3, 4, 5, 6, 7, 15, 8, 3, 10, 10, 11, 3, 12, 17, 15 };

    }
}
