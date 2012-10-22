using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.GameObjects
{
    public class Enemy : CollisionSystem.GameObjectCollidable
    {
        protected Texture2D image;
        protected Game game;
        protected SpriteBatch spriteBatch;
        protected ObjectManager objectManager;

        Point frameSize = new Point(300, 300);
        Point collisionBoxReduction = new Point(50,50);
        Point currentFrame = new Point(0, 0);
        Point imageSize = new Point(3, 3);
        Point velocity = new Point(1,3); //test

        int timeSinceLastFrame = 0;
        int msPerFrame = 200;

        public Enemy(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int)position.X - collisionBoxReduction.X,
                (int)position.Y - collisionBoxReduction.Y,
                frameSize.X,
                frameSize.Y);
        }

        public void LoadContent()
        {
            //image = game.Content.Load<Texture2D>(@"Images/Enemies/vortex");
            //collisionBox.Width = (image.Width / imageSize.X) - (collisionBoxReduction.X * 2);
            //collisionBox.Height = (image.Height / imageSize.Y) - (collisionBoxReduction.Y * 2);
            objectManager = (ObjectManager)game.Services.GetService(typeof(ObjectManager));
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > msPerFrame)
            {
                timeSinceLastFrame -= msPerFrame;

                ++currentFrame.X;
                if (currentFrame.X >= imageSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= imageSize.Y)
                        currentFrame.Y = 0;
                }
            }

            position.X += velocity.X;
            position.Y += velocity.Y;

            collisionBox.X = (int)position.Y;
            collisionBox.Y = (int)position.X;

            
            collisionBox.X = (int) (position.X + collisionBoxReduction.X);
            collisionBox.Y = (int)(position.Y + collisionBoxReduction.Y);
            /*
            collisionBox.Width += collisionBoxReduction.X;
            collisionBox.Height += collisionBoxReduction.Y;
            */

            if (collisionBox.X <= 0)
                velocity.X = -velocity.X;
            if (collisionBox.X + collisionBox.Width >= game.Window.ClientBounds.Width)
                velocity.X = -velocity.X;
            if (collisionBox.Y <= 0)
                velocity.Y = -velocity.Y;
            if (collisionBox.Y + collisionBox.Height >= game.Window.ClientBounds.Height)
                velocity.Y = -velocity.Y;
            
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, 
                new Vector2( position.X, position.Y), 
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    0);
        }

    }

}
