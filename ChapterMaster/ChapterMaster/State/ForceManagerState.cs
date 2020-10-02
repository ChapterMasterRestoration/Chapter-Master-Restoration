using ChapterMaster.Tree;
using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using ChapterMaster.UI.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.State
{
    public class ForceManagerState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        ForceManagerScreen screen;
        Force force;
        public ForceManagerState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager, Force force) : base(gameManager, graphicsDevice, contentManager)
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
            screen = new ForceManagerScreen(0, "spr_rock_bg_0", new MapFrameAlign(0, 0, 0, 0), false);
            screen.primitive = new PrimitiveBuddy.Primitive(graphicsDevice, SpriteBatch);
            this.force = force;
            screen.Force = force;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Resize(GameWindow window)
        {
            throw new NotImplementedException();
        }

    }
}