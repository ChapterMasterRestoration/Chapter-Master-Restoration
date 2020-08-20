using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.World
{
    public enum Type
    {
        LAVA = 0,
        TEMPERATE = 1,
        DESERT = 2,
        FORGE = 3,
        HIVE = 4,
        DEATH = 5,
        AGRI = 6,
        FEUDAL = 8,
        ICE = 10,
        WATER = 11,
        DEAD = 12,
        DAEMON = 14,
        SHRINE = 15,
        SPACEHULK = 16
    }

    public class Planet
    {
        Type type;
        int population;


        public static int TypeToTexture(Type type)
        {
            if (type == Type.TEMPERATE) return (int) Type.FEUDAL; // Same bloody texture.
            return (int)type;
        }
    }
}
