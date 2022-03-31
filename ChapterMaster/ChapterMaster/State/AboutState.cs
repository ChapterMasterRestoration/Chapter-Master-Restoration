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
    public class AboutState : State
    {
        MenuViewController aboutViewController;
        Desktop _about;
        public AboutState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
            SpriteBatch = new SpriteBatch(graphicsDevice);
            aboutViewController = new MenuViewController();
            GameManager.graphics.PreferredBackBufferWidth = GameManager.window.ClientBounds.Width;
            GameManager.graphics.PreferredBackBufferHeight = GameManager.window.ClientBounds.Height;
            aboutViewController.viewPortWidth = GameManager.window.ClientBounds.Width;
            aboutViewController.viewPortHeight = GameManager.window.ClientBounds.Height;
            GameManager.graphics.ApplyChanges(); // I'm not questioning why this works. I, Cato Sicarius, approve of this action, because I, Cato Sicarius, am the most well versed Captain when it comes to the Codex Astartes!            


            _about = new Desktop();

            var CreatedBy = new Label 
            {
                Text = "Created By: sewer rat (obscuredcode) and SirNuke",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 600,
                Height = 100,
            };

            _about.Widgets.Add(CreatedBy);
        }


        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            _about.Render();
            SpriteBatch.End();
        }

        public override void Resize(GameWindow window)
        {
            aboutViewController.viewPortWidth = window.ClientBounds.Width;
            aboutViewController.viewPortHeight = window.ClientBounds.Height;
        }

        public override Desktop GetDesktop()
        {
            throw new System.NotImplementedException();
        }
    }
}
