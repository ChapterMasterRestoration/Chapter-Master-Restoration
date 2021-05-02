using ChapterMaster.Render;
using ChapterMaster.State;
using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(0, 0, 0));
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(0, 1, 0));
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(2, 1, 1));
                ChapterMaster.Sector.Fleets.Add(new Fleet.Fleet(0, 1, 1));
                ChapterMaster.Sector.Finalize();
            }
            renderer = new SectorRenderer();
            ChapterMaster.ViewController = new ViewController();
            spriteBatch = new SpriteBatch(graphicsDevice);
            ChapterMaster.MainScreen = new Screen(0, "mapframe", new MapFrameAlign(11, 61, 11, 10), false); // TO DO: The proletariat will rise. Enable advanced occlusion for the map frame.
            //ChapterMaster.ViewController.viewPortWidth = GameManager.GetWidth();
            //ChapterMaster.ViewController.viewPortHeight = GameManager.GetHeight();
            GameManager.graphics.PreferredBackBufferWidth = GameManager.window.ClientBounds.Width;
            GameManager.graphics.PreferredBackBufferHeight = GameManager.window.ClientBounds.Height;
            ChapterMaster.ViewController.viewPortWidth = GameManager.window.ClientBounds.Width;
            ChapterMaster.ViewController.viewPortHeight = GameManager.window.ClientBounds.Height;
            GameManager.graphics.ApplyChanges(); // I'm not questioning why this works. ;) #relatable
            // 144 43
            ChapterMaster.MainScreen.AddButton(new Button(0, "End Turn", new CornerAlign(Corner.BOTTOMRIGHT, 144, 43), new MouseHandler(EndTurn)));
            renderer.Initialize(graphicsDevice, spriteBatch);
            RenderHelper.Initialize(graphicsDevice, spriteBatch);
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
            // TODO: Add your drawing code here
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
            //MainScreen.Rect = new Rectangle(0, 0, GetWidth(), GetHeight()); // TO DO: implement scaling properly in Screen class.
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
