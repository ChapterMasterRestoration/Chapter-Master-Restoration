using ChapterMaster.Render;
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
        SectorRenderer renderer;
        public static ViewController view; // i hate this
        public static SpriteFont Caslon_Antique_Regular;
        public static SpriteFont Caslon_Antique_Bold;
        public static SpriteFont ARJULIAN;
        public static SpriteFont Courier_New;
        private Texture2D background;
        public static Dictionary<string,Texture2D> UITextures;
        public static Texture2D[] ButtonTextures = new Texture2D[5];
        public static Texture2D[] SystemTextures = new Texture2D[6];
        public static Texture2D[][] FleetTextures = new Texture2D[11][];
        public static Texture2D[] PlanetTextures = new Texture2D[16];
        public static Texture2D[] PlanetTypeTextures = new Texture2D[18];
        public static string DebugString = "";
        public static Sector sector = new Sector(); // refactor properly
        public static Screen MainScreen;

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
            sector.TurnUpdate();
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
            // idk what minimum distance we should do
            sector.GridGenerate(50, 100, Constants.SystemSize, Constants.WorldWidth, Constants.WorldHeight);
            sector.WarpLaneGenerate();
            sector.GenerateSystemNames();
            sector.GeneratePlanets();
            sector.Fleets.Add(new Fleet.Fleet(0,0,0));
            sector.Fleets.Add(new Fleet.Fleet(0, 1, 0));
            sector.Fleets.Add(new Fleet.Fleet(2, 1, 1));
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
            #region Load Batches
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            #endregion
            Loader.CONTENT_ROOT = Content.RootDirectory;
            #region Load Fonts
            Caslon_Antique_Regular = this.Content.Load<SpriteFont>("font/caslon-antique.regular");
            Caslon_Antique_Bold = this.Content.Load<SpriteFont>("font/caslon-antique.bold");
            ARJULIAN = this.Content.Load<SpriteFont>("font/ARJULIAN");
            Courier_New = this.Content.Load<SpriteFont>("font/cour");
            #endregion
            #region Load UI Textures
            UITextures = new Dictionary<string, Texture2D>();
            background = Loader.LoadPNG("background/bg_space");
            UITextures.Add("mapframe",Loader.LoadPNG("spr_new_ui_1"));
            UITextures.Add("systemscreen1", Loader.LoadPNG("spr_star_screen_1"));
            UITextures.Add("systemscreen2", Loader.LoadPNG("spr_star_screen_2"));
            UITextures.Add("systemscreen3", Loader.LoadPNG("spr_star_screen_3"));
            UITextures.Add("systemscreen4", Loader.LoadPNG("spr_star_screen_4"));
            UITextures.Add("planetscreen", Loader.LoadPNG("spr_planet_screen_1")); // modified texture by removing extra space

            for (int i = 0; i < ButtonTextures.Length - 1; i++)
            {
                ButtonTextures[i] = Loader.LoadPNG("spr_ui_but_" + (i + 1) + "_0");
            }
            ButtonTextures[4] = Loader.LoadPNG("spr_pin_button");
            for (int i = 1; i < PlanetTypeTextures.Length; i++)
            {
                PlanetTypeTextures[i] = Loader.LoadPNG("ui/planet" + i);
            }
            #endregion
            #region Load Game Textures
            for (int i = 0; i < SystemTextures.Length; i++)
            {
                SystemTextures[i] = Loader.LoadPNG("spr_star_" + i);
            }
            for (int faction = 0; faction < Constants.FleetTexture.Length; faction++)
            {
                FleetTextures[faction] = new Texture2D[Constants.FleetStateLimit[faction]];
                for (int state = 0; state < Constants.FleetStateLimit[faction]; state++) 
                {
                    FleetTextures[faction][state] = Loader.LoadPNG("spr_fleet_" + Constants.FleetTexture[faction] + "_" + state);
                }
            }
            for (int i = 0; i < PlanetTextures.Length; i++)
            {
                PlanetTextures[i] = Loader.LoadPNG("spr_planets_" + i);
            }
            #endregion
            renderer = new SectorRenderer();
            view = new ViewController();
            MainScreen = new Screen(0, "mapframe", new MapFrameAlign(11,61,11,10));
            view.viewPortWidth = GetWidth();
            view.viewPortHeight = GetHeight();
            // 144 43
            MainScreen.AddButton(new Button(0, "End Turn",new CornerAlign(Corner.BOTTOMRIGHT,144,43), new MouseHandler(EndTurn)));
            renderer.Initialize(GraphicsDevice, spriteBatch);
            RenderHelper.Initialize(GraphicsDevice, spriteBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // idk fonts are bound to be destroyed GC.
            spriteBatch.Dispose();
            #region Unload UI Textures
            background.Dispose();
            foreach(KeyValuePair<string,Texture2D> texture in UITextures)
            {
                texture.Value.Dispose();
            }
            for (int i = 0; i < ButtonTextures.Length - 1; i++)
            {
                ButtonTextures[i].Dispose();
            }
            for (int i = 1; i < PlanetTypeTextures.Length; i++)
            {
                PlanetTypeTextures[i].Dispose();
            }
            #endregion
            #region Unload Game Textures
            for (int i = 0; i < SystemTextures.Length; i++)
            {
                SystemTextures[i].Dispose();
            }
            for (int faction = 0; faction < Constants.FleetTexture.Length; faction++)
            {
                FleetTextures[faction] = new Texture2D[Constants.FleetStateLimit[faction]];
                for (int state = 0; state < Constants.FleetStateLimit[faction]; state++)
                {
                    if(!(FleetTextures[faction][state] is null)) FleetTextures[faction][state].Dispose();
                }
            }
            #endregion
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();
            if (Keyboard.GetState().IsKeyUp(Keys.E)) buttonDown = false;
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !buttonDown)
            {
                sector.GridGenerate(50, 100, Constants.SystemSize, Constants.WorldWidth, Constants.WorldHeight);
                sector.WarpLaneGenerate();
                sector.GenerateSystemNames();
                sector.GeneratePlanets();
                buttonDown = true;
            }
            view.MouseSelection(sector);
            view.Update();
            MainScreen.Update(view);
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
                  BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null);
            // Draw background // Background is not tiled, merely scaled.
            spriteBatch.Draw(background, 
                //new Rectangle(0, 0,Constants.WorldWidth,Constants.WorldHeight), 
                new Rectangle(0, 0, GetWidth(), GetHeight()),
                Color.White);
            // Draw systems // spr_star_0 to spr_star_5
            renderer.Render(spriteBatch, sector,view);
            // Draw warp lanes
            // Draw UI
            //MainScreen.Rect = new Rectangle(0, 0, GetWidth(), GetHeight()); // TODO: implement scaling properly in Screen class.
            MainScreen.Render(spriteBatch, view);
            spriteBatch.DrawString(ARJULIAN, DebugString, new Vector2(0, 100), Color.White);
            spriteBatch.End();
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
            view.viewPortWidth = Window.ClientBounds.Width;
            view.viewPortHeight = Window.ClientBounds.Height;
            graphics.ApplyChanges();
        }
    }
}
