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
        Point currentFrame = new Point(0, 0);
        Point imageSize = new Point(3, 3);

        int timeSinceLastFrame = 0;
        int msPerFrame = 200;


        public Enemy(Game baseGame, SpriteBatch spriteBatchToUse, Vector2 position)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 0, 0);
        }

        public void LoadContent()
        {
            image = game.Content.Load<Texture2D>(@"Images/Enemies/vortex");
            collisionBox.Width = image.Width;
            collisionBox.Height = image.Height;
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
            

            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, 
                Vector2.Zero,
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
