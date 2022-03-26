using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    public static class Assets
    {
        private static SpriteFont caslon_Antique_Regular;
        private static SpriteFont caslon_Antique_Bold;
        private static SpriteFont aRJULIAN;
        private static SpriteFont courier_New;
        public static Texture2D Background;
        private static Dictionary<string, Texture2D> uITextures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Texture2D> buttonTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Texture2D> iconTextures = new Dictionary<string, Texture2D>();
        public static Texture2D[] SystemTextures = new Texture2D[6];
        public static Texture2D[][] FleetTextures = new Texture2D[11][];
        public static Texture2D[] PlanetTextures = new Texture2D[16];
        public static Texture2D[] PlanetTypeTextures = new Texture2D[18];
        public static Texture2D LoadingScreen;

        public static SpriteFont CaslonAntiqueRegular { get => caslon_Antique_Regular; set => caslon_Antique_Regular = value; }
        public static SpriteFont CaslonAntiqueBold { get => caslon_Antique_Bold; set => caslon_Antique_Bold = value; }
        public static SpriteFont ARJULIAN { get => aRJULIAN; set => aRJULIAN = value; }
        public static SpriteFont CourierNew { get => courier_New; set => courier_New = value; }
        public static Dictionary<string, Texture2D> UITextures { get => uITextures;}
        public static Dictionary<string, Texture2D> ButtonTextures { get => buttonTextures;}
        public static Dictionary<string, Texture2D> IconTextures { get => iconTextures; }

        public static Texture2D GetTexture(string id)
        {
            if (id == "background") return Background;
            return uITextures[id];
        }

        public static Texture2D GetButton(string id)
        {
            return buttonTextures[id];
        }
        public static Texture2D GetIcon(string id)
        {
            return iconTextures[id];
        }
    }
}
