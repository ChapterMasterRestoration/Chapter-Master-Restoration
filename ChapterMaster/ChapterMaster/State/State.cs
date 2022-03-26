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
    public abstract class State
    {
        public SpriteBatch SpriteBatch;
        public State(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {

        }

        #region Marching Song
        // We march as one,
        // We march for all,
        // We march towards the Somme!
        // We march as one,
        // We march until it's done,
        // We march 'till we beat back the Hun!
        // Hey! Ho! Hey! Ho!
        // Victory's ours tonight!
        // Hey! Ho! Hey! Ho!
        #endregion

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
        public abstract void Resize(GameWindow window);
    }
}
