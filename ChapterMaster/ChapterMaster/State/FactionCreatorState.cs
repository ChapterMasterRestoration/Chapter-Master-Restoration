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

namespace ChapterMaster.State
{
    public class FactionCreatorState : State
    {
        GameManager gameManager;
        GraphicsDevice graphicsDevice;
        private MenuViewController viewController;
        FactionScreen screen;
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
            CornerAlign c = new CornerAlign(Corner.TOPLEFT, 250, 50, 64); // , screen.factionAlign
            CornerAlign exit = new CornerAlign(Corner.BOTTOMLEFT, 128, 32, 64); //This button does not want to be put into subAlign. Finish adjusting CornerAlign
            //Button b = new Button(10, "", c, NewSpaceMarineChapter); Replace with other button definition.
            Button e = new Button(9, "", exit, Back);
            //screen.AddButton(b);
            Textbox textbox = new Textbox(11, "", c, textboxClick);
            screen.AddButton(textbox);
            //screen.AddButton(e);
        }
        private void textboxClick(MouseState mouseState, object sender)
        {
            //((Textbox)sender).Check(viewController, ((Textbox)sender).align);
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
