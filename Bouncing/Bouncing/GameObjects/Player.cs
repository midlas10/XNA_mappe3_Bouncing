using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Bouncing
{
    public class Player : GameObjectCollidable
    {
        SoundEffect soundEffect;
        SoundEffectInstance soundeffectinstance;

        protected float movementPerSecond = (float)500;
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

        protected bool dead;
        protected int StarsCollected;

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
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 100, 100);
            dead = false;
            StarsCollected = 0;
        }

        public void LoadContent()
        {

            soundEffect = game.Content.Load<SoundEffect>(@"Audio/star");
            soundeffectinstance = soundEffect.CreateInstance();



            image = game.Content.Load<Texture2D>(@"Images/Player/blob");
            collisionBox.Width = 100;
            collisionBox.Height = 100;

            objectManager = (ObjectManager) game.Services.GetService(typeof (ObjectManager));
            collisionManager = (IManageCollisionsService) game.Services.GetService((typeof (IManageCollisionsService)));
            _input = (IInputService) game.Services.GetService(typeof (IInputService));

           
        }

        public override void Update(GameTime gameTime)
        {
            currentFrame.X = 1;
            currentFrame.Y = 1;

            #region Player Input
            if ((_input.IsKeyDown(Keys.D) || _input.IsKeyDown(Keys.Right)) && (_input.IsKeyDown(Keys.W) || _input.IsKeyDown(Keys.Up)))
            {
                if (game.Window.ClientBounds.Width >= position.X + collisionBox.Width)
                {
                    position.X += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (position.Y >= 0)
                {
                    position.Y -= movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 2;
                currentFrame.Y = 0;
            }
            else if ((_input.IsKeyDown(Keys.D) || _input.IsKeyDown(Keys.Right)) && (_input.IsKeyDown(Keys.S) || _input.IsKeyDown(Keys.Down)))
            {
                if (game.Window.ClientBounds.Width >= Position.X + collisionBox.Width)
                {
                    position.X += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (game.Window.ClientBounds.Height >= position.Y + collisionBox.Height)
                {
                    position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 2;
                currentFrame.Y = 2;
            }
            else if ((_input.IsKeyDown(Keys.A) || _input.IsKeyDown(Keys.Left)) && (_input.IsKeyDown(Keys.S) || _input.IsKeyDown(Keys.Down)))
            {
                if (position.X >= 0)
                {
                    position.X -= movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (game.Window.ClientBounds.Height >= position.Y + collisionBox.Height)
                {
                    position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 0;
                currentFrame.Y = 2;
            }
            else if ((_input.IsKeyDown(Keys.A) || _input.IsKeyDown(Keys.Left)) && (_input.IsKeyDown(Keys.W) || _input.IsKeyDown(Keys.Up)))
            {
                if (position.X >= 0)
                {
                    position.X -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (position.Y >= 0)
                {
                    position.Y -= movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 0;
                currentFrame.Y = 0;
            }


            else if(_input.IsKeyDown(Keys.W) || _input.IsKeyDown(Keys.Up))
            {
                if (position.Y >= 0)
                {
                    position.Y -= movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 1;
                currentFrame.Y = 0;
            } 
            else if(_input.IsKeyDown(Keys.S) || _input.IsKeyDown(Keys.Down))
            {
                if (game.Window.ClientBounds.Height >= position.Y + collisionBox.Height)
                {
                    position.Y += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 1;
                currentFrame.Y = 2;
            }

            else if(_input.IsKeyDown(Keys.A) || _input.IsKeyDown(Keys.Left))
            {
                if (position.X >= 0)
                {
                    position.X -= movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 0;
                currentFrame.Y = 1;
            }
            else if (_input.IsKeyDown(Keys.D) || _input.IsKeyDown(Keys.Right))
            {
                if (game.Window.ClientBounds.Width >= position.X + collisionBox.Width)
                {
                    position.X += movementPerSecond*(float) gameTime.ElapsedGameTime.TotalSeconds;
                }
                currentFrame.X = 2;
                currentFrame.Y = 1;
            }
            #endregion

            collisionBox = new Rectangle((int)position.X, (int)position.Y, 100, 100);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

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

            base.Draw(gameTime);
        }

        public override void Collision(GameObjectCollidable goc)
        {
            if(goc as Vortex != null)
            {
                dead = true;
                return;
            }
            if(goc as Star != null)
            {
                StarsCollected++;
                //System.Console.Write("Star collision");

                soundeffectinstance.Volume = 0.9f;
                soundeffectinstance.Play();

                return;
            }
        }

        public bool IsDead()
        {
            return dead;
        }

        public int GetTotalStarsCollected()
        {
            return StarsCollected;
        }
    }
}
