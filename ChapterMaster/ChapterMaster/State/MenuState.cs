using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.State
{
    public class MenuState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController menuViewController;
        Screen menuScreen;
        public MenuState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
            this.gameManager = gameManager;
            this.graphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
            menuViewController = new MenuViewController();
            GameManager.graphics.PreferredBackBufferWidth = GameManager.window.ClientBounds.Width;
            GameManager.graphics.PreferredBackBufferHeight = GameManager.window.ClientBounds.Height;
            menuViewController.viewPortWidth = GameManager.window.ClientBounds.Width;
            menuViewController.viewPortHeight = GameManager.window.ClientBounds.Height;
            GameManager.graphics.ApplyChanges(); // I'm not questioning why this works. I, Cato Sicarius, approve of this action, because I, Cato Sicarius, am the most well versed Captain when it comes to the Codex Astartes!
            menuScreen = new Screen(0, "title_splash", new MapFrameAlign(0, 0, 0, 0), false);
            menuScreen.AddButton(new Button(5, "", new CornerAlign(Corner.BOTTOMLEFT, 256, 48, 50, 0, 0, 20), NewGame));
        }

        private void NewGame(MouseState mouseState, object sender)
        {
            gameManager.ChangeState(new GameState(gameManager, gameManager.GraphicsDevice, gameManager.Content, false));
        }

        public override void Update(GameTime gameTime)
        {
            menuScreen.Update(menuViewController);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            menuScreen.Render(SpriteBatch, menuViewController);
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            menuViewController.viewPortWidth = window.ClientBounds.Width;
            menuViewController.viewPortHeight = window.ClientBounds.Height;
        }
    }
}
