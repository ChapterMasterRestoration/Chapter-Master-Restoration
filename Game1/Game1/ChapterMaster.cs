using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ChapterMaster
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ChapterMaster : Game
    {
        GraphicsDeviceManager graphics;
        public static GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SectorRenderer renderer;
        int WIDTH = 1280;
        int HEIGHT = 960;
        private Texture2D background;
        public static Texture2D[] SystemTextures = new Texture2D[6];
        
        public ChapterMaster()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        // metathesis
        /* FileStream file = new FileStream("Content/images/spr_honor_helm_3.png", FileMode.Open);
        graphicsDevice = graphics.GraphicsDevice;
        test = Texture2D.FromStream(graphicsDevice, file);
        file.Close(); */

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            graphicsDevice = graphics.GraphicsDevice;
            Loader.CONTENT_ROOT = Content.RootDirectory;
            background = Loader.LoadPNG("background/bg_space");
            for (int i = 0; i < SystemTextures.Length; i++)
            {
                SystemTextures[i] = Loader.LoadPNG("spr_star_" + i);
            }
            renderer = new SectorRenderer();
            renderer.Initialize();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // check for End Turn button click
            base.Update(gameTime);
        }

        System system = new System(1);
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            // Draw each loop
            // Move and Rotate Camera
            float camX = 0; 
            float camY = 0;
            // Scale and Zoom
            float scaleX = 1;
            float scaleY = 1;
            // Draw background
            spriteBatch.Draw(background, new Rectangle(0, 0,1280,960), Color.White);
            float zoom = 1;

            // Draw systems // spr_star_0 to spr_star_5
            renderer.DrawStar(spriteBatch, new Vector2(400, 400), system);
            // Draw warp lanes
            // Draw UI
            renderer.DrawLine(spriteBatch, new Vector2(50, 50), new Vector2(200, 200), Color.Red);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
