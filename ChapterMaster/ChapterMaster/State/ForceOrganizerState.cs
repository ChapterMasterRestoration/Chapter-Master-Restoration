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
    public class ForceOrganizerState : State
    {
        public ForceOrganizerState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Resize(GameWindow window)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
