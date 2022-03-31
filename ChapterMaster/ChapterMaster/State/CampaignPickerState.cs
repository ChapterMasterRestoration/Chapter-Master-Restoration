using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using ChapterMaster.UI.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;

namespace ChapterMaster.State
{
    public class CampaignPickerState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        //CampaignPicker screen;

        Desktop _campaignPicker;
        public CampaignPickerState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
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

            _campaignPicker = new Desktop();

            var Panel = new Panel { Background = new TextureRegion(Assets.GetTexture("faction_creator_background"))};

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
                gameManager.ChangeState(new MenuState(gameManager, gameManager.GraphicsDevice, gameManager.Content));
            };

            Panel.AddChild(BackButton);

            var CampaignPicker = new Panel
            {
                Background = new TextureRegion(Assets.GetTexture("campaign_picker")),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 400,
                Height = 500
            };
            CampaignPicker.Layout2d.Expresion = "this.w=W.w*0.45;this.h=W.h*0.95";

            var CampaignButtons = new VerticalStackPanel 
            {
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var SpaceMarineTexture = new TextureRegion(Assets.GetButton("space_marine"));
            var SpaceMarine = new ImageButton
            {
                Background = SpaceMarineTexture,
                OverImage = SpaceMarineTexture,
                Width = 64,
                Height = 128,
                Margin = new Thickness(40,40,0,0)
            };

            SpaceMarine.TouchDown += (s, a) =>
            {
                gameManager.ChangeState(new FactionCreatorState(gameManager, gameManager.GraphicsDevice, gameManager.Content));
            };

            CampaignButtons.AddChild(SpaceMarine);
            CampaignPicker.AddChild(CampaignButtons);
            Panel.AddChild(CampaignPicker);
            _campaignPicker.Widgets.Add(Panel);

            _campaignPicker.InvalidateLayout();
            _campaignPicker.UpdateLayout();
        }



        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            _campaignPicker.Render();
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            viewController.viewPortWidth = window.ClientBounds.Width;
            viewController.viewPortHeight = window.ClientBounds.Height;
            _campaignPicker.InvalidateLayout();
            _campaignPicker.UpdateLayout();
        }

        public override Desktop GetDesktop()
        {
            return _campaignPicker;
        }
    }
}
