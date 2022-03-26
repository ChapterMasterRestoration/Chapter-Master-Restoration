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

using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;

namespace ChapterMaster.State
{
    public class FactionCreatorState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        //FactionScreen screen;
        private Faction playerFaction;

        Desktop _factionCreator;

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


            _factionCreator = new Desktop();

            var Panel = new Panel { Background = new TextureRegion(Assets.GetTexture("faction_creator_background")) };

            var BackButtonTexture = new TextureRegion(Assets.GetButton("back"));
            var BackButton = new ImageButton
            {
                Background = BackButtonTexture,
                OverImage = BackButtonTexture,
                Width = 128,
                Height = 32,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(20, 0, 0, 20)
            };
            BackButton.TouchDown += (s, a) =>
            {
                gameManager.ChangeState(new CampaignPickerState(gameManager, gameManager.GraphicsDevice, gameManager.Content));
            };
            Panel.AddChild(BackButton);

            var StartButtonTexture = new TextureRegion(Assets.GetButton("ui_but_0"));
            var StartButton = new TextButton
            {
                //Background = StartButtonTexture,
                //OverImage = StartButtonTexture,
                Text = "Start",
                TextColor = Color.White,
                Width = 128,
                Height = 32,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 20, 20)
            };
            StartButton.TouchDown += (s, a) =>
            {
                // Initialize Sector with Faction information.
                playerFaction = new Faction();
                playerFaction.Name = "TODO"; // TODO
                ChapterMaster.Sector.Factions.Add(playerFaction.Name, playerFaction);
                ChapterMaster.Sector.CurrentFaction = playerFaction.Name;
                gameManager.ChangeState(new GameState(gameManager, gameManager.GraphicsDevice, gameManager.Content, false));
            };
            Panel.AddChild(StartButton);

            var FactionPicker = new Panel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

         

            //Button exitButton = new Button("back", "", new CornerAlign(Corner.BOTTOMLEFT, 128, 32, 64), Back); //This button does not want to be put into subAlign. Finish adjusting CornerAlign
            //Button startButton = new Button("ui_but_0", "START", new CornerAlign(Corner.BOTTOMRIGHT, 128, 32, rightMargin:64), Start);
            //TextBox factionNameBox = new TextBox("textbox", "Faction Name", new CornerAlign(Corner.TOPLEFT, 250, 50, leftMargin:64,topMargin:20), outOfFocus:FactionName);
            //TextBox homeWorldNameBox = new TextBox("textbox", "Homeworld Name", new CornerAlign(Corner.TOPLEFT, 250, 50, leftMargin:64,topMargin:100), outOfFocus:HomeWorldName);
            Panel.AddChild(FactionPicker);
            _factionCreator.Widgets.Add(Panel);
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

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            _factionCreator.Render();
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            viewController.viewPortWidth = window.ClientBounds.Width;
            viewController.viewPortHeight = window.ClientBounds.Height;
        }
    }
}
