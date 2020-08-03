using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace ChapterMaster
{
    class Loader
    {
        public static string CONTENT_ROOT;

        public static Texture2D LoadPNG(string name)
        {
            Texture2D texture;
            FileStream file = new FileStream(CONTENT_ROOT + "/" + Constants.IMAGE_DIRECTORY + "/" + name + ".png", FileMode.Open);
            texture = Texture2D.FromStream(ChapterMaster.graphicsDevice, file);
            file.Close();
            return texture;
        }
    }
}
