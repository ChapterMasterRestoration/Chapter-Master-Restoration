using ChapterMaster.Tree;
using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using ChapterMaster.World;
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
    class GroundCombatState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        GroundCombatScreen screen;
        public List<Squad> currentSquads;

        public GroundCombatState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager, Planet planet) : base(gameManager, graphicsDevice, contentManager)
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
            screen = new GroundCombatScreen(0, "bg_combat_grass", new MapFrameAlign(0, 0, 0, 0), planet, false);
            screen.primitive = new PrimitiveBuddy.Primitive(graphicsDevice, SpriteBatch);
            Squad squad = new Squad(planet, GenerateTroops(4));
            for (int i = 0; i < squad.Troops.Count; i++)
            {
                Troop troop = squad.Troops[i];
                troop.Position = new Vector2(0, troop.Size.Y * 2) * i;
            }
            screen.squad = squad;
        }

        private List<Troop> GenerateTroops(int n)
        {
            List<Troop> troops = new List<Troop>();
            for (int i = 0; i < n; i++)
            {
                troops.Add(new Troop());
            }
            return troops;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameManager.ChangeState(new GameState(gameManager, graphicsDevice, gameManager.Content, true));
            }
            screen.Update(viewController);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            screen.Render(SpriteBatch, viewController);
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            ChapterMaster.ViewController.viewPortWidth = window.ClientBounds.Width;
            ChapterMaster.ViewController.viewPortHeight = window.ClientBounds.Height;
            viewController.viewPortWidth = window.ClientBounds.Width;
            viewController.viewPortHeight = window.ClientBounds.Height;
        }

    }
}
