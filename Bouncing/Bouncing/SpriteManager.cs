using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bouncing
{
    class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Player _player;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            _player = new Player(Game.Content.Load<Texture2D>("tull"), 
                Vector2.Zero,
                Color.White);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime, Game.Window.ClientBounds);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        }
    }
}
