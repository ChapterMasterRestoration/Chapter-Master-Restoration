using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    public class GameManager : Game
    {
        public static GameWindow window;
        public static GraphicsDeviceManager graphics;
        public static GraphicsDevice graphicsDevice;
        private State.State currentState;
        private State.State nextState;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            window = Window;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(WindowResized);
            Content.RootDirectory = "Content";
        }

        public void ChangeState(State.State state)
        {
            nextState = state;
        }
        public State.State GetState()
        {
            return currentState;
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
            graphicsDevice = GraphicsDevice;
            currentState = new State.LoadingState(this, GraphicsDevice, Content);
            Debug.WriteLine("loading state created");
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {

        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // idk fonts are bound to be destroyed GC.
            //currentState.SpriteBatch.Dispose();
            #region Unload UI Textures
            Assets.Background.Dispose();
            foreach (KeyValuePair<string, Texture2D> texture in Assets.UITextures)
            {
                texture.Value.Dispose();
            }
            for (int i = 0; i < Assets.ButtonTextures.Length - 1; i++)
            {
                Assets.ButtonTextures[i].Dispose();
            }
            for (int i = 1; i < Assets.PlanetTypeTextures.Length; i++)
            {
                Assets.PlanetTypeTextures[i].Dispose();
            }
            #endregion
            #region Unload Game Textures
            for (int i = 0; i < Assets.SystemTextures.Length; i++)
            {
                Assets.SystemTextures[i].Dispose();
            }
            for (int faction = 0; faction < Constants.FleetTexture.Length; faction++)
            {
                Assets.FleetTextures[faction] = new Texture2D[Constants.FleetStateLimit[faction]];
                for (int state = 0; state < Constants.FleetStateLimit[faction]; state++)
                {
                    if (!(Assets.FleetTextures[faction][state] is null)) Assets.FleetTextures[faction][state].Dispose();
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
        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            if (currentState != null)
            {
                currentState.Update(gameTime);
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (currentState != null)
            {
                currentState.Draw(gameTime);
            }
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
            currentState.Resize(Window);
            graphics.ApplyChanges();
        }
        public static void Quit()
        {
            this.Exit();
        }
    }
}
