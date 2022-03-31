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

            var FactionPickerBackground = new TextureRegion(Assets.GetTexture("campaign_picker"));
            var FactionPicker = new Panel
            {
                Background = FactionPickerBackground,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                //Width = 800,
                //Height = 1000,
            };
            FactionPicker.Layout2d.Expresion = "this.w=W.w*0.45;this.h=W.h*0.95"; // TODO SET THIS UP

            var FactionVertical = new VerticalStackPanel
            {
                //HorizontalAlignment = HorizontalAlignment.Center
            };

            var SelectFactionLabel = new Label
            {
                Text = "Select Chapter",
                Font = Assets.CaslonAntiqueBoldFSS.GetFont(32),
                TextColor = new Color(0, 143, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                //Width = ,
                Height = 80,
                Margin = new Thickness(0, 40, 0, 0)
            };
            FactionVertical.AddChild(SelectFactionLabel);


            var FoundingChaptersLabel = new Label
            {
                Text = "Founding Chapters",
                Font = Assets.CaslonAntiqueBoldFSS.GetFont(32),
                TextColor = new Color(0, 143, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                //Width = ,
                Height = 80,
                Margin = new Thickness(20, 0, 0, 0)
            };
            FactionVertical.AddChild(FoundingChaptersLabel);

            var FoundingChapters = new Grid
            {
                ColumnSpacing = 16,
                RowSpacing = 30,
                ShowGridLines = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(15, 0, 0, 0)
            };

            for(int i = 1; i < 10; i ++)
            {
                var ChapterTexture = new TextureRegion(Assets.GetIcon("founding_chapter_" + i));
                
                var ChapterButton = new ImageButton
                {
                    Background = ChapterTexture,
                    OverImage = ChapterTexture,
                    GridColumn = i - 1
                    //Width = 32,
                    //Height = 32,
                };
                ChapterButton.Layout2d.Expresion = "this.w = W.w*0.04; this.h = W.h*0.04";

                ChapterButton.TouchDown += (s, a) =>
                {
                    // TODO: set up faction chosed by player
                };
                FoundingChapters.AddChild(ChapterButton);

            }
            FactionVertical.AddChild(FoundingChapters);

            var SuccessorChaptersLabel = new Label
            {
                Text = "Successor Chapters",
                Font = Assets.CaslonAntiqueBoldFSS.GetFont(32),
                TextColor = new Color(0, 143, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                //Width = ,
                Height = 80,
                Margin = new Thickness(20, 0, 0, 0)
            };
            FactionVertical.AddChild(SuccessorChaptersLabel);

            var SuccessorChapters = new Grid
            {
                ColumnSpacing = 16,
                RowSpacing = 30,
                ShowGridLines = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(15, 0, 0, 0)
            };
            
            for(int i = 1; i < 5; i++)
            {
                var ChapterTexture = new TextureRegion(Assets.GetIcon("successor_chapter_" + i));

                var ChapterButton = new ImageButton
                {
                    Background = ChapterTexture,
                    OverImage = ChapterTexture,
                    GridColumn = i - 1,
                    //Width = 32,
                    //Height = 32,
                };
                ChapterButton.Layout2d.Expresion = "this.w = W.w*0.04; this.h = W.h*0.04";

                ChapterButton.TouchDown += (s, a) =>
                {
                    // TODO: set up faction chosed by player
                };
                SuccessorChapters.AddChild(ChapterButton);
            }


            FactionVertical.AddChild(SuccessorChapters);


            var CustomFactionsLabel = new Label
            {
                Text = "Custom Factions",
                Font = Assets.CaslonAntiqueBoldFSS.GetFont(32),
                TextColor = new Color(0, 143, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                //Width = ,
                Height = 80,
                Margin = new Thickness(20, 0, 0, 0)
            };
            FactionVertical.AddChild(CustomFactionsLabel);

            var CustomFactions = new Grid
            {
                ColumnSpacing = 16,
                RowSpacing = 30,
                ShowGridLines = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(15, 0, 0, 0)
            };

            var CustomFactionButton = new TextButton
            {
                Text = "Create Marine Chapter",
                TextColor = Color.White,
                Width = 128,
                Height = 32,
                //VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(20, 0, 0, 0)
            };

            CustomFactionButton.TouchDown += (s, e) =>
            {

            };
            CustomFactions.AddChild(CustomFactionButton);

            FactionVertical.AddChild(CustomFactions);


            var OrkKlansLabel = new Label
            {
                Text = "Ork Klans",
                Font = Assets.CaslonAntiqueBoldFSS.GetFont(32),
                TextColor = new Color(0, 143, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                //Width = ,
                Height = 80,
                Margin = new Thickness(20, 0, 0, 0)
            };

            FactionVertical.AddChild(OrkKlansLabel);

            var OrkKlans = new Grid
            {
                ColumnSpacing = 16,
                RowSpacing = 30,
                ShowGridLines = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(15, 0, 0, 0)
            };

            FactionVertical.AddChild(OrkKlans);

            //Button exitButton = new Button("back", "", new CornerAlign(Corner.BOTTOMLEFT, 128, 32, 64), Back); //This button does not want to be put into subAlign. Finish adjusting CornerAlign
            //Button startButton = new Button("ui_but_0", "START", new CornerAlign(Corner.BOTTOMRIGHT, 128, 32, rightMargin:64), Start);
            //TextBox factionNameBox = new TextBox("textbox", "Faction Name", new CornerAlign(Corner.TOPLEFT, 250, 50, leftMargin:64,topMargin:20), outOfFocus:FactionName);
            //TextBox homeWorldNameBox = new TextBox("textbox", "Homeworld Name", new CornerAlign(Corner.TOPLEFT, 250, 50, leftMargin:64,topMargin:100), outOfFocus:HomeWorldName);

            FactionPicker.AddChild(FactionVertical);
            Panel.AddChild(FactionPicker);
            _factionCreator.Widgets.Add(Panel);

           
            _factionCreator.InvalidateLayout();
            _factionCreator.UpdateLayout();
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
            _factionCreator.InvalidateLayout();
            _factionCreator.UpdateLayout();
        }

        public override Desktop GetDesktop()
        {
            return _factionCreator;
        }
    }
}
