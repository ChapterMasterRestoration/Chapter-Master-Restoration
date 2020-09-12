using ChapterMaster.UI;
using ChapterMaster.UI.Align;
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
        LAVA = 0, // 1,2
        TEMPERATE = 1, // 9
        DESERT = 2, // 3
        FORGE = 3, // 4
        HIVE = 4, // 5
        DEATH = 5, // 6
        AGRI = 6, // 7
        NECRON = 7, // 14 // special!
        FEUDAL = 8, // 8
        DESERT2 = 9, // 3 like desert for now
        ICE = 10, // 10
        WATER = 11, // 10 same as ice?
        DEAD = 12, // 11
        DESERT3 = 13, // 3 like desert for now
        DAEMON = 14, // 12
        SHRINE = 15, // 17
        SPACEHULK = 16 // 15 placeholder
    }
    /* -1 = undefined 13 = eldar craftworld, */
    
    public class Planet
    {
        public int systemId;
        public int planetId;
        public Type Type;
        public int Population;
        public string FactionOwner; // Planetary ownership could be extended later on with combat. Dictionary<string Faction, float controlpercentage>

        public Planet(Type Type, int systemId, int planetId, string FactionOwner = "Imperium") // Ave Imperator.
        {
            this.Type = Type;
            this.systemId = systemId;
            this.planetId = planetId;
            this.FactionOwner = FactionOwner;
        }
        public static int TypeToTexture(Type type)
        {
            if (type == Type.TEMPERATE) return (int) Type.FEUDAL; // Same bloody texture.
            return (int)type;
        }
        public string GetTypeName()
        {
            return Constants.PlanetTypeNames[(int)Type];
        }
        public int GetTypeTexture()
        {
            return Constants.PlanetTypeTextures[(int)Type];
        }
        public void OpenPlanetScreen(ViewController view, SystemScreen parentScreen)
        {
            //Debug.WriteLine("planet in system " + systemId);
            parentScreen.AddChildScreen(new PlanetScreen(2, "planetscreen", new PlanetScreenAlign(parentScreen, planetId), systemId, planetId));
        }
        public void ClosePlanetScreen(ViewController view)
        {
            //Debug.WriteLine("closing planet in system " + systemId);
        }
    }
}
