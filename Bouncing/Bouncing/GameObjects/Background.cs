using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing
{
    public class Background : GameObject
    {
        protected Texture2D backgroundArt;

        protected float rotation;
        protected int SpriteWidth;
        protected int SpriteHeight;
        protected SpriteBatch spriteBatch;
        protected Rectangle DrawRectangle;

        public Background(Texture2D back, SpriteBatch spriteBatch)
        {
            backgroundArt = back;
            this.spriteBatch = spriteBatch;
            DrawRectangle = new Rectangle(0, 0, backgroundArt.Width, backgroundArt.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(backgroundArt, DrawRectangle, Color.White);
        }
    }
}
