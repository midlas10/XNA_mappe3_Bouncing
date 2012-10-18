/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bouncing
{
    class SpriteManager : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Player _player;
        List<Sprite> enemies = new List<Sprite>();

        SpriteManager(Game theGame) : base(theGame)
        {

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            _player = new Player(Game.Content.Load<Texture2D>("Tull"), 
                Vector2.Zero,
                new Point(300, 300),
                10,
                new Point(1,1),
                new Point(900, 900),
                new Vector2(6,6));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime, Game.Window.ClientBounds);
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Draw();
        }
    }
}

*/