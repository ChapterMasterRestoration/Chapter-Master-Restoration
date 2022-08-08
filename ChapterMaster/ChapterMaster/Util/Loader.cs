using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace ChapterMaster
{
    class Loader
    {
        public static string CONTENT_ROOT;

        // based on https://stackoverflow.com/questions/32429219/procedurally-generating-a-texture2d-in-xna-monogame
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //set the color
            texture.SetData(data);

            return texture;
        }

        public static Texture2D LoadPNG(string name)
        {
            Texture2D texture;
            try
            {
                FileStream file = new FileStream(CONTENT_ROOT + "/" + Constants.ImageDirectory + "/" + name + ".png", FileMode.Open);
                texture = Texture2D.FromStream(GameManager.graphicsDevice, file);
                file.Close();
            }
            catch (Exception)
            {
                texture = CreateTexture(GameManager.graphicsDevice, 64,64, pixel => pixel % 2 == 1 || ((pixel % 3 == 1) && pixel/64 % 2 == 1) ? Color.Purple : Color.Black);
                Debug.WriteLine("Cannot find file " + name + " in " + CONTENT_ROOT + "/" + Constants.ImageDirectory + "/" + name + ".png");
            }
            
            return texture;
        }
    }
}
