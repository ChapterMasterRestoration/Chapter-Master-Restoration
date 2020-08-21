using ChapterMaster.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int systemId;
        public int planetId;
        public Type Type;
        public int Population;

        public Planet(Type Type, int systemId, int planetId)
        {
            this.Type = Type;
            this.systemId = systemId;
            this.planetId = planetId;
        }
        public static int TypeToTexture(Type type)
        {
            if (type == Type.TEMPERATE) return (int) Type.FEUDAL; // Same bloody texture.
            return (int)type;
        }
        public void OpenPlanetScreen(ViewController view)
        {
            Debug.WriteLine("planet in system " + systemId);
            foreach(Screen parentScreen in ChapterMaster.MainScreen.Screens)
            {
                if (parentScreen is PlanetsScreen) {
                    if (((PlanetsScreen)parentScreen).systemId != systemId)
                    {
                        ((PlanetsScreen)parentScreen).AddChildScreen(new PlanetScreen(2, "",new PlanetScreenAlign(systemId), systemId, planetId));
                    }
                }
            }
        }
        public void ClosePlanetScreen(ViewController view)
        {
            Debug.WriteLine("closing planet in system " + systemId);
        }
    }
}
