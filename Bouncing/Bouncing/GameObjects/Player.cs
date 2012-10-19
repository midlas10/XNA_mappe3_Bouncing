using Bouncing.CollisionSystem;
using Bouncing.Input;
using Bouncing.Managers;
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

        protected Texture2D image;
        protected Rectangle playerAreal;

        protected float rotation;
        protected int SpriteWidth;
        protected int SpriteHeight;
        protected Rectangle sourceRectangle;

        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected IInputService _input;

        Point frameSize = new Point(300, 300);
        Point currentFrame = new Point(0, 0);
        Point imageSize = new Point(3, 3);

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
<<<<<<< HEAD
            playerArt = game.Content.Load<Texture2D>(@"Images/Player/blob");
            collisionBox.Width = 20;
            collisionBox.Height = 20;
=======
            image = game.Content.Load<Texture2D>(@"Images/Player/blob");
            collisionBox.Width = image.Width;
            collisionBox.Height = image.Height;
>>>>>>> MenuScreens etc.
            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));
        }

        public override void Update(GameTime gameTime)
        {
            currentFrame.X = 1;
            currentFrame.Y = 1;

            if(_input.IsKeyDown(Keys.W) || _input.IsKeyDown(Keys.Up))
            {
                ChangeAnimation(7);
                position.Y -= movementPerSecond*(float)gameTime.ElapsedGameTime.TotalSeconds;

                currentFrame.X = 1;
                currentFrame.Y = 0;
            } 
            else if(_input.IsKeyDown(Keys.S) || _input.IsKeyDown(Keys.Down))
            {
                ChangeAnimation(1);
                position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;

                currentFrame.X = 1;
                currentFrame.Y = 2;
            }

            else if(_input.IsKeyDown(Keys.A) || _input.IsKeyDown(Keys.Left))
            {
                ChangeAnimation(5);
                position.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                currentFrame.X = 0;
                currentFrame.Y = 1;
            }
            else if (_input.IsKeyDown(Keys.D) || _input.IsKeyDown(Keys.Right))
            {
                ChangeAnimation(3);
                position.X += movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;

                currentFrame.X = 2;
                currentFrame.Y = 1;
            }
<<<<<<< HEAD
            rotation += 40 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, SpriteHeight);
=======

            collisionBox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            
>>>>>>> MenuScreens etc.
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
<<<<<<< HEAD
            spriteBatch.Draw(playerArt, collisionBox, sourceRectangle, Color.White, 0f, new Vector2(playerArt.Width / 3, playerArt.Height / 3), SpriteEffects.None, 1f);
=======
            spriteBatch.Draw(image, 
                collisionBox, 
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y), 
                    Color.White, 
                    0f, 
                    Vector2.Zero, 
                    SpriteEffects.None, 
                    0);

>>>>>>> MenuScreens etc.
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
