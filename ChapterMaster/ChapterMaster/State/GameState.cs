using ChapterMaster.Render;
using ChapterMaster.State;
using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace ChapterMaster
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameState : State.State
    {
        GameManager gameManager;
        public GraphicsDevice GraphicsDevice;
        public ContentManager ContentManager;
        SpriteBatch spriteBatch;
        SectorRenderer renderer;
        Ledger Ledger;
        public GameState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager, bool preserveState) : base(gameManager, graphicsDevice, contentManager)
        {
            this.gameManager = gameManager;
            this.GraphicsDevice = graphicsDevice;
            this.ContentManager = contentManager;
            if (!preserveState)
            {
                ChapterMaster.Sector.Prepare();
                // idk what minimum distance we should do
                //ChapterMaster.Sector.GridGenerate(50, 100, Constants.SystemSize, Constants.WorldWidth, Constants.WorldHeight);
                ChapterMaster.Sector.clusterGenerate();
                ChapterMaster.Sector.WarpLaneGenerate();
                ChapterMaster.Sector.GenerateSystemNames();
                ChapterMaster.Sector.GeneratePlanets();
                string factionName = ChapterMaster.Sector.CurrentFaction;
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(0, 0, 0));
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(0, 0, 0));
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(2, 0, 1));
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(0, 0, 1));
                ChapterMaster.Sector.Finalize();
            }
            renderer = new SectorRenderer();
            ChapterMaster.ViewController = new ViewController();
            spriteBatch = new SpriteBatch(graphicsDevice);
            //ChapterMaster.ViewController.viewPortWidth = GameManager.GetWidth();
            //ChapterMaster.ViewController.viewPortHeight = GameManager.GetHeight();
            GameManager.graphics.PreferredBackBufferWidth = GameManager.window.ClientBounds.Width;
            GameManager.graphics.PreferredBackBufferHeight = GameManager.window.ClientBounds.Height;
            ChapterMaster.ViewController.viewPortWidth = GameManager.window.ClientBounds.Width;
            ChapterMaster.ViewController.viewPortHeight = GameManager.window.ClientBounds.Height;
            GameManager.graphics.ApplyChanges(); // I'm not questioning why this works. ;) #relatable
            renderer.Initialize(graphicsDevice, spriteBatch);
            RenderHelper.Initialize(graphicsDevice, spriteBatch);
            
            ChapterMaster.MainScreen = new Screen(0,
                "mapframe",
                new MapFrameAlign(11,
                    61,
                    156,
                    10),
                false); // TODO: The proletariat will rise. Enable advanced occlusion for the map frame.
            ChapterMaster.MainScreen.AddButton(new Button("ui_but_0",
                "End Turn",
                new CornerAlign(Corner.BOTTOMRIGHT,
                    144,
                    43),
                EndTurn));
            Ledger = new Ledger(1, "ledger_background", new CornerAlign(Corner.BOTTOMRIGHT, 120, 280,topMargin: 10,leftMargin:2,bottomMargin:58));
            ChapterMaster.MainScreen.AddChildScreen(Ledger);
        }

        private void EndTurn(MouseState mouseState, object sender)
        {
            ChapterMaster.Sector.TurnUpdate();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        bool buttonDown = false;
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                gameManager.ChangeState(new ForceOrganizerState(gameManager, GraphicsDevice, gameManager.Content));
                return;
            }
            ChapterMaster.ViewController.UpdateMouse();
            ChapterMaster.ViewController.UpdateKeyboard();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                GameManager.Quit();
            if (Keyboard.GetState().IsKeyUp(Keys.E)) buttonDown = false;
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !buttonDown)
            {
                //ChapterMaster.Sector.GridGenerate(50, 100, Constants.SystemSize, Constants.WorldWidth, Constants.WorldHeight);
                ChapterMaster.Sector.clusterGenerate();
                ChapterMaster.Sector.WarpLaneGenerate();
                ChapterMaster.Sector.GenerateSystemNames();
                ChapterMaster.Sector.GeneratePlanets();
                buttonDown = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Home))
            {
                if (ChapterMaster.Sector.GetHomeSystem() != null)
                {
                    World.System system = ChapterMaster.Sector.GetHomeSystem();
                    ChapterMaster.ViewController.camX = system.x;
                    ChapterMaster.ViewController.camY = system.y;
                    // TODO: Select home system?
                }
            }
            ChapterMaster.ViewController.MouseSelection(ChapterMaster.Sector);
            ChapterMaster.DebugString += "\n" + ChapterMaster.Sector.GetImperialDate();
            ChapterMaster.MainScreen.Update(ChapterMaster.ViewController);
            ChapterMaster.ViewController.Update();
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                  BlendState.NonPremultiplied, SamplerState.LinearWrap, null, null);
            // Draw background // Background is not tiled, merely scaled.
            spriteBatch.Draw(Assets.Background,
                //new Rectangle(0, 0,Constants.WorldWidth,Constants.WorldHeight), 
                new Rectangle(0, 0, GameManager.GetWidth(), GameManager.GetHeight()),
                Color.White);
            // Draw systems // spr_star_0 to spr_star_5
            renderer.Render(spriteBatch, ChapterMaster.Sector, ChapterMaster.ViewController);
            // Draw warp lanes
            // Draw UI
            //MainScreen.Rect = new Rectangle(0, 0, GetWidth(), GetHeight()); // TODO implement scaling properly in Screen class.
            ChapterMaster.ViewController.animationDelta = (float) (100.0 * gameTime.ElapsedGameTime.TotalSeconds);
            ChapterMaster.MainScreen.Render(spriteBatch, ChapterMaster.ViewController);
            spriteBatch.DrawString(Assets.ARJULIAN, ChapterMaster.DebugString, new Vector2(0, 100), Color.White);
            spriteBatch.End();
        }
        public override void Resize(GameWindow window)
        {
            ChapterMaster.ViewController.viewPortWidth = window.ClientBounds.Width;
            ChapterMaster.ViewController.viewPortHeight = window.ClientBounds.Height;
        }
    }
}
