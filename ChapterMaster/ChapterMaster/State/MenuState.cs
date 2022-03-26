using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;

namespace ChapterMaster.State
{
    public class MenuState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController menuViewController;
        //Screen menuScreen;

        private Desktop _menu;

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
            

            _menu = new Desktop();

            var panel = new Panel();
            var BackGroundImage = new TextureRegion(Assets.GetTexture("title_splash")); // , new Rectangle(0,0, 1600, 900)
            panel.Background = BackGroundImage;

            var Buttons = new HorizontalStackPanel {Spacing = 200, 
                                                                   HorizontalAlignment = HorizontalAlignment.Center, 
                                                                   VerticalAlignment = VerticalAlignment.Bottom,
                                                                   Margin = new Thickness(0,0,0,60) 
                                                    };

            var NewGameTexture = new TextureRegion(Assets.GetButton("new_game"));
            var LoadGameTexture = new TextureRegion(Assets.GetButton("load_game"));

            Stylesheet.Current.ButtonStyle.PressedBackground = new SolidBrush("#0000000"); // TODO: rethink this later
            //Stylesheet.Current.ButtonStyle.OverBackground = new SolidBrush("#0000000");

            var NewGameButton = new ImageButton
            {
                Background = NewGameTexture,
                OverImage = NewGameTexture,
                Width = 256,
                Height = 48,

            };
            
            NewGameButton.TouchDown += (s, a) => {
                gameManager.ChangeState(new CampaignPickerState(gameManager, gameManager.GraphicsDevice, gameManager.Content));
            };

            Buttons.Widgets.Add(NewGameButton);

            var LoadGameButton = new ImageButton
            {
                Background = LoadGameTexture,
                OverImage = LoadGameTexture,
                Width = 256,
                Height = 48
            };

            LoadGameButton.TouchDown += (s, a) => {
                gameManager.ChangeState(new GameState(gameManager, gameManager.GraphicsDevice, gameManager.Content, false));
            };
           

            Buttons.Widgets.Add(LoadGameButton);

            panel.Widgets.Add(Buttons);
            _menu.Root = panel;
        }


        public override void Update(GameTime gameTime)
        {
            //menuScreen.Update(menuViewController);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            //menuScreen.Render(SpriteBatch, menuViewController);
            _menu.Render();
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            menuViewController.viewPortWidth = window.ClientBounds.Width;
            menuViewController.viewPortHeight = window.ClientBounds.Height;
        }
    }
}
