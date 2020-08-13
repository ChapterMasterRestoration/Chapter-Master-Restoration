using ChapterMaster.UI;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChapterMaster
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ChapterMaster : Game
    {
        public static GameWindow window;
        GraphicsDeviceManager graphics;
        public static GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteBatch fontBatch;
        SectorRenderer renderer;
        ViewController view;
        public static SpriteFont caslon_antique_regular;
        public static SpriteFont ARJULIAN;
        private Texture2D background;
        private Texture2D mapframe;
        public static Texture2D[] ButtonTextures = new Texture2D[4];
        public static Texture2D[] SystemTextures = new Texture2D[6];
        public static Texture2D[][] FleetTextures = new Texture2D[11][];
        Sector sector = new Sector();
        Screen mainScreen = new Screen();

        public ChapterMaster()
        {
            graphics = new GraphicsDeviceManager(this);
            window = Window;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(WindowResized);
            Content.RootDirectory = "Content";
        }

        private void EndTurn(MouseState mouseState, object sender)
        {
            Debug.WriteLine("Your Opinion is Objectively Wrong.");
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
            this.IsMouseVisible = true;
            Window.Title = "Chapter Master Revived";
            sector.Prepare();
            // 1280 960
            sector.GridGenerate(50, 100, Constants.SYSTEM_WIDTH_HEIGHT, Constants.WorldWidth, Constants.WorldHeight);
            sector.WarpLaneGenerate();
            sector.Fleets.Add(new Fleet.Fleet());
            // Initialize UI
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fontBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            graphicsDevice = graphics.GraphicsDevice;
            Loader.CONTENT_ROOT = Content.RootDirectory;
            caslon_antique_regular = this.Content.Load<SpriteFont>("font/caslon-antique.regular"); 
            ARJULIAN = this.Content.Load<SpriteFont>("font/ARJULIAN");
            background = Loader.LoadPNG("background/bg_space");
            mapframe = Loader.LoadPNG("spr_new_ui_0");
            for (int i = 0; i < ButtonTextures.Length; i++)
            {
                ButtonTextures[i] = Loader.LoadPNG("spr_ui_but_" + (i + 1) + "_0");
            }
            for (int i = 0; i < SystemTextures.Length; i++)
            {
                SystemTextures[i] = Loader.LoadPNG("spr_star_" + i);
            }
            for (int faction = 0; faction < Constants.FLEET_TEXTURE_ID_FILE.Length; faction++)
            {
                FleetTextures[faction] = new Texture2D[Constants.FLEET_STATE_LIMIT[faction]];
                for (int state = 0; state < Constants.FLEET_STATE_LIMIT[faction]; state++) 
                {
                    FleetTextures[faction][state] = Loader.LoadPNG("spr_fleet_" + Constants.FLEET_TEXTURE_ID_FILE[faction] + "_" + state);
                }
            }
            renderer = new SectorRenderer();
            view = new ViewController();
            mainScreen.Buttons.Add(new UI.Button(0, "End Turn", new Vector2(GetWidth() - 154, GetHeight() - 65), new Vector2(144, 43), new MouseHandler(EndTurn)));
            renderer.Initialize(graphicsDevice,spriteBatch);
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
        bool buttonDown = false;
        protected override void Update(GameTime gameTime) {


            view.UpdateMouse();
            view.UpdateKeyboard();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyUp(Keys.E)) buttonDown = false;
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !buttonDown)
            {
                sector.GridGenerate(50, 100, Constants.SYSTEM_WIDTH_HEIGHT, Constants.WorldWidth, Constants.WorldHeight);
                sector.WarpLaneGenerate();
                buttonDown = true;
            }
            view.MouseSelection(sector);
            view.Update();
            mainScreen.Update(view);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                  null, SamplerState.LinearWrap, null, null);
            // Draw background // Background is not tiled, merely scaled.
            spriteBatch.Draw(background, 
                //new Rectangle(0, 0,Constants.WorldWidth,Constants.WorldHeight), 
                new Rectangle(0, 0, GetWidth(), GetHeight()),
                Color.White);
            // Draw systems // spr_star_0 to spr_star_5
            renderer.Render(spriteBatch, sector,view);
            // Draw warp lanes
            // Draw UI
            mainScreen.Render(spriteBatch, view);
            spriteBatch.Draw(mapframe, new Rectangle(0, 0, GetWidth(), GetHeight()),Color.White);
            spriteBatch.End();
            //fontBatch.Begin();
            
            //fontBatch.End();
            base.Draw(gameTime);
        }
        public static int GetWidth()
        {
            return graphicsDevice.Viewport.Width;
        }
        public static int GetHeight()
        {
            return graphicsDevice.Viewport.Height;
        }
        public void WindowResized(object sender, EventArgs args)
        {
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.ApplyChanges();
        }
    }
}
