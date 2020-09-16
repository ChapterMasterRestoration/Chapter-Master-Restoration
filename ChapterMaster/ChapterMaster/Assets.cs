using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    public class Assets
    {
        public static SpriteFont Caslon_Antique_Regular;
        public static SpriteFont Caslon_Antique_Bold;
        public static SpriteFont ARJULIAN;
        public static SpriteFont Courier_New;
        public static Texture2D Background;
        public static Dictionary<string, Texture2D> UITextures;
        public static Texture2D[] ButtonTextures = new Texture2D[10]; // TO DO: Change to dictionary.
        public static Texture2D[] SystemTextures = new Texture2D[6];
        public static Texture2D[][] FleetTextures = new Texture2D[11][];
        public static Texture2D[] PlanetTextures = new Texture2D[16];
        public static Texture2D[] PlanetTypeTextures = new Texture2D[18];
        public static Texture2D LoadingScreen;
    }
}
