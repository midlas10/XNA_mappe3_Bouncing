using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bouncing.CollisionSystem;
using Bouncing.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bouncing.GameObjects
{
    public class Player : GameObjectCollidable
    {
        protected float movementPerSecond = (float)60;
        protected SpriteBatch spriteBatch;
        protected Game game;

        protected Texture2D playerArt;
        protected Rectangle playerAreal;

        protected float rotation;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected IInputService _input;

        public Player(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 0, 0);
        }

        public void LoadContent()
        {
            playerArt = game.Content.Load<Texture2D>("Tull");
            collisionBox.Width = playerArt.Width;
            collisionBox.Height = playerArt.Height;
            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));
        }

        public override void Update(GameTime gameTime)
        {
            if(_input.IsKeyDown(Keys.W))
            {
                position.Y -= movementPerSecond*(float)gameTime.ElapsedGameTime.TotalSeconds;
            } else if(_input.IsKeyDown(Keys.S))
            {
                position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(_input.IsKeyDown(Keys.A))
            {
                position.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_input.IsKeyDown(Keys.D))
            {
                position.X += movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            rotation += 40 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, playerArt.Width, playerArt.Height);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(playerArt, collisionBox, new Rectangle(0, 0, playerArt.Width, playerArt.Height), Color.White, rotation + MathHelper.PiOver2, new Vector2(playerArt.Width / 2, playerArt.Height / 2), SpriteEffects.None, 1f);
            base.Draw(gameTime);
        }
    }
}
