using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing
{
    class Sprite
    {
        Texture2D image;

        protected Point imageSize;
        protected Point frameSize;
        protected Point currentFrame;
        protected Vector2 position, speed;


        public Sprite()
        {

        }

        public abstract Vector2 direction
        {
            get;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

    }
}
