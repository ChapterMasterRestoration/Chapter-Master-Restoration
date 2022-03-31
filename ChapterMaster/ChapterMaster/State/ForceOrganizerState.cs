using ChapterMaster.Combat;
using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.State
{
    public class ForceOrganizerState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        ForceOrganizerScreen screen;

        Combat.Tree tree;

        public ForceOrganizerState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
            this.gameManager = gameManager;
            this.graphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
            viewController = new MenuViewController();
            GameManager.graphics.PreferredBackBufferWidth = GameManager.window.ClientBounds.Width;
            GameManager.graphics.PreferredBackBufferHeight = GameManager.window.ClientBounds.Height;
            viewController.viewPortWidth = GameManager.window.ClientBounds.Width;
            viewController.viewPortHeight = GameManager.window.ClientBounds.Height;
            GameManager.graphics.ApplyChanges(); // I'm not questioning why this works. I, Cato Sicarius, approve of this action, because I, Cato Sicarius, am the most well versed Captain when it comes to the Codex Astartes!
            screen = new ForceOrganizerScreen(0, "spr_rock_bg_0", new MapFrameAlign(0, 0, 0, 0), false);
            screen.primitive = new PrimitiveBuddy.Primitive(graphicsDevice, SpriteBatch);
            tree = new Combat.Tree();
            tree.Parent = new Force("HQ", 400, 20);
            tree.Parent.AddChildren(new Force("Deathwing Inner Circle", 200, 100).AddChildren(new Force("1st Coy. 'Deathwing'", 200, 280)), new Force("Ravenwing Inner Circle", 600, 100).AddChildren(new Force("2nd Coy. 'Ravenwing'", 600, 280)));
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameManager.ChangeState(new GameState(gameManager, graphicsDevice, gameManager.Content, true));
            }
            screen.Update(viewController, tree);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            screen.Render(SpriteBatch, viewController, tree);
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            ChapterMaster.ViewController.viewPortWidth = window.ClientBounds.Width;
            ChapterMaster.ViewController.viewPortHeight = window.ClientBounds.Height;
            viewController.viewPortWidth = window.ClientBounds.Width;
            viewController.viewPortHeight = window.ClientBounds.Height;
        }

        public override Desktop GetDesktop()
        {
            throw new NotImplementedException();
        }
    }
}
