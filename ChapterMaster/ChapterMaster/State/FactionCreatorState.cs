using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using ChapterMaster.UI.State;
using ChapterMaster.UI.State.FactionCreator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapterMaster.World.Faction;

namespace ChapterMaster.State
{
    public class FactionCreatorState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        FactionScreen screen;
        private Faction playerFaction;
        public FactionCreatorState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
            this.gameManager = gameManager;
            this.graphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
            viewController = new MenuViewController();

            GameManager.graphics.PreferredBackBufferWidth = GameManager.window.ClientBounds.Width;
            GameManager.graphics.PreferredBackBufferHeight = GameManager.window.ClientBounds.Height;
            viewController.viewPortWidth = GameManager.window.ClientBounds.Width;
            viewController.viewPortHeight = GameManager.window.ClientBounds.Height;
            GameManager.graphics.ApplyChanges(); // I'm not questioning why this works.

            screen = new FactionScreen(0, "black_background", new MapFrameAlign(0, 0, 0, 0), false);
            screen.primitive = new PrimitiveBuddy.Primitive(graphicsDevice, SpriteBatch);
           
            playerFaction = new Faction();
            
            Button exitButton = new Button("back", "", new CornerAlign(Corner.BOTTOMLEFT, 128, 32, 64), Back); //This button does not want to be put into subAlign. Finish adjusting CornerAlign
            Button startButton = new Button("ui_but_0", "START", new CornerAlign(Corner.BOTTOMRIGHT, 128, 32, rightMargin:64), Start);
            TextBox factionNameBox = new TextBox("textbox", "Faction Name", new CornerAlign(Corner.TOPLEFT, 250, 50, leftMargin:64,topMargin:20), outOfFocus:FactionName);
            TextBox homeWorldNameBox = new TextBox("textbox", "Homeworld Name", new CornerAlign(Corner.TOPLEFT, 250, 50, leftMargin:64,topMargin:100), outOfFocus:HomeWorldName);
            screen.AddButton(factionNameBox);
            screen.AddButton(homeWorldNameBox);
            screen.AddButton(exitButton);
            screen.AddButton(startButton);
        }

        private void FactionName(object sender, string value)
        {
            // TODO: Sanitize player faction name.
            playerFaction.Name = value;
        }
        private void HomeWorldName(object sender, string value)
        {
            // TODO: Sanitize player faction name.
            playerFaction.HomeSystemName = value;
        }
        private void Start(MouseState mouseState, object sender)
        {
            // Initialize Sector with Faction information.
            ChapterMaster.Sector.Factions.Add(playerFaction.Name, playerFaction);
            ChapterMaster.Sector.CurrentFaction = playerFaction.Name;
            gameManager.ChangeState(new GameState(gameManager, gameManager.GraphicsDevice, gameManager.Content, false));

        }
        private void Back(MouseState mouseState, object sender)
        {
            gameManager.ChangeState(new CampaignPickerState(gameManager, gameManager.GraphicsDevice, gameManager.Content));
        }

        public override void Update(GameTime gameTime)
        {
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
            viewController.viewPortWidth = window.ClientBounds.Width;
            viewController.viewPortHeight = window.ClientBounds.Height;
        }
    }
}
