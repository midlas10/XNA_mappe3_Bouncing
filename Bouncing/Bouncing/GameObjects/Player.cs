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
        protected int SpriteWidth;
        protected int SpriteHeight;
        protected Rectangle sourceRectangle;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected IInputService _input;

        public Player(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            SpriteHeight = 300;
            SpriteWidth = 300;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, SpriteWidth, SpriteHeight);
        }

        public void LoadContent()
        {
            playerArt = game.Content.Load<Texture2D>(@"Images/Player/blob");
            collisionBox.Width = 20;
            collisionBox.Height = 20;
            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));
        }

        public override void Update(GameTime gameTime)
        {
            if(_input.IsKeyDown(Keys.W) || _input.IsKeyDown(Keys.Up))
            {
                ChangeAnimation(7);
                position.Y -= movementPerSecond*(float)gameTime.ElapsedGameTime.TotalSeconds;
            } 
            else if(_input.IsKeyDown(Keys.S) || _input.IsKeyDown(Keys.Down))
            {
                ChangeAnimation(1);
                position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(_input.IsKeyDown(Keys.A) || _input.IsKeyDown(Keys.Left))
            {
                ChangeAnimation(5);
                position.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_input.IsKeyDown(Keys.D) || _input.IsKeyDown(Keys.Right))
            {
                ChangeAnimation(3);
                position.X += movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            rotation += 40 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, SpriteHeight);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(playerArt, collisionBox, sourceRectangle, Color.White, 0f, new Vector2(playerArt.Width / 3, playerArt.Height / 3), SpriteEffects.None, 1f);
            base.Draw(gameTime);
        }

        protected void ChangeAnimation(int anim)
        {
            if(anim >= 6)
            {
                sourceRectangle = new Rectangle(SpriteWidth * (8 - anim), SpriteHeight * 0, SpriteWidth, SpriteHeight);
            } else if(anim >= 3)
            {
                sourceRectangle = new Rectangle(SpriteWidth * (5 - anim), SpriteHeight * 1, SpriteWidth, SpriteHeight);
            } else 
            {
                sourceRectangle = new Rectangle(SpriteWidth * (2 - anim), SpriteHeight * 2, SpriteWidth, SpriteHeight);
            }
        }
    }
}
