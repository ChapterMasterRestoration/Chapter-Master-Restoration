using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChapterMaster.State
{
    using Asset = Tuple<string, int>;
    public class LoadingState : State
    {
        Stack<Asset> assetsToLoad;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        PrimitiveBuddy.Primitive primitive;
        int progress;

        public LoadingState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            assetsToLoad.Push(new Asset("Fleet Textures", 17));
            assetsToLoad.Push(new Asset("Background", 15));
            assetsToLoad.Push(new Asset("Planet Climate Textures", 18));
            assetsToLoad.Push(new Asset("Planet Textures", 10));
            assetsToLoad.Push(new Asset("System Textures", 5));

            assetsToLoad.Push(new Asset("Button Textures", 5));
            assetsToLoad.Push(new Asset("UI Textures", 25));
            assetsToLoad.Push(new Asset("Fonts", 5));

            Loader.CONTENT_ROOT = contentManager.RootDirectory;
            spriteBatch = new SpriteBatch(graphicsDevice);
            #region Load Fonts
            Assets.Caslon_Antique_Regular = contentManager.Load<SpriteFont>("font/caslon-antique.regular");
            Assets.Caslon_Antique_Bold = contentManager.Load<SpriteFont>("font/caslon-antique.bold");
            Assets.ARJULIAN = contentManager.Load<SpriteFont>("font/ARJULIAN");
            Assets.Courier_New = contentManager.Load<SpriteFont>("font/cour");
            #endregion
            primitive = new PrimitiveBuddy.Primitive(graphicsDevice, spriteBatch);
            Assets.LoadingScreen = Loader.LoadPNG("loading/loading1");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Assets.LoadingScreen, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
            DrawProgressBar(spriteBatch, graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2, 200, 80, progress, Color.Gray, Color.Yellow);
            spriteBatch.End();
        }

        private void DrawProgressBar(SpriteBatch spriteBatch, int x, int y, int width, int height, int progress, Color background, Color fill)
        {
            primitive.Rectangle(new Rectangle(x, y, width, height), background);
            primitive.Rectangle(new Rectangle(x+2, y+2, (width-2)*(progress/100), height-2), fill);
        }

        public override void Update(GameTime gameTime)
        {
            if (assetsToLoad.Count > 0)
            {
                Asset asset = assetsToLoad.Pop();
                switch (asset.Item1)
                {
                    case "Fleet Textures":
                        break;
                    case "Background":
                        break;
                    case "Planet Climate Textures":
                        break;
                    case "Planet Textures":
                        break;
                    case "System Textures":
                        break;
                    case "Button Textures":
                        break;
                    case "UI Textures":
                        break;
                    case "Fonts":
                        break;
                    default:
                        throw new Exception($"Asset type {asset.Item1} is not recognized");
                }
            }
        }
    }
}
