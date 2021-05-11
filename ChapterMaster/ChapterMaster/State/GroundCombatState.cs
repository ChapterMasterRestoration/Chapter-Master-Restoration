using ChapterMaster.Combat;
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
        List<Squad> playerSquads = new List<Squad>();
        List<Squad> alliedSquads = new List<Squad>();
        List<Squad> enemySquad = new List<Squad>();
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
            screen.Primitive = new PrimitiveBuddy.Primitive(graphicsDevice, SpriteBatch);
            // Squads are going to be selected by the player through the Attack screen.
            playerSquads.Clear();
            playerSquads.Add(new Squad(planet, GenerateTroops(4), "Space Marine"));
            playerSquads.Add(new Squad(planet, GenerateTroops(12), "Space Marine"));
            //playerSquads.Add(new Squad(planet, GenerateTroops(8)));
            GenerateGrid(playerSquads);
            screen.Squads = playerSquads;
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

        private List<Squad> GenerateGrid(List<Squad> squads, int size = 2)
        {
            squads.Sort((a, b) => { return a.Troops.Count.CompareTo(b.Troops.Count); }); // unsure what order to put squads in
            for (int currentSquad = 0; currentSquad < squads.Count; currentSquad++)
            {
                Squad squad =squads[currentSquad];
                #region Failed Grid Attempts
                // int squadX = 5 + (31 * 2 ) * numberOfSquads; // TODO Find with size of biggest troop.
                //int noColumns = squad.Troops.Count % 5 == 0 ? squad.Troops.Count / 5 : (squad.Troops.Count / 5) + 1;
                //for (int currentColumn = 0; currentColumn < noColumns; currentColumn++)
                //{
                //    int noRow = 5;
                //    if (currentColumn == noColumns - 1 && noColumns % 5 != 0)
                //    {
                //        noRow = noColumns % 5;
                //    }
                //    for (int currentRow = 0; currentRow < noRow - 1; currentRow++)
                //    {
                //        Troop troop = squad.Troops[(currentColumn * 5) + currentRow];
                //        troop.Position = new Vector2(31 * currentColumn + 36, (10 + troop.Size.Y * 2) * currentRow);
                //    }
                //}
                #endregion
                int count = squad.Troops.Count;
                int noColumns = count % size == 0 ? count / size : (count / size) + 1;
                int squadTop = 10 + (size * (31 + 20) + size) * currentSquad;
                squad.Position = new Vector2(10, squadTop);
                for (int currentTroop = 0; currentTroop < count; currentTroop++)
                {
                    Troop troop = squad.Troops[currentTroop];
                    //int currentCol = noColumns - (currentTroop % 5);
                    //int currentRow = (currentTroop / 5);
                    //troop.Position = new Vector2(10 + currentRow * (troop.Size.X + 15) - noColumns * (5),
                    //                             (10 + currentCol * (troop.Size.Y + 15)));]
                    int currentCol = (currentTroop % size);
                    int currentRow = (currentTroop / size);
                    troop.Position = new Vector2(currentRow * (troop.Size.X + 25),
                                                 (currentCol * (troop.Size.Y + 15)));
                }
            }

            return squads;
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
