using Bouncing.CollisionSystem;
using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.GameObjects
{
    public class Star : GameObjectCollidable
    {
        protected SpriteBatch spriteBatch;
        protected Game game;
        protected float movementPerSecond = (float)60;
        protected Texture2D starArt;
        protected static ObjectManager omReference;

        public Star(Game baseGame, Vector2 position, SpriteBatch spriteBatchToUse)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            starArt = game.Content.Load<Texture2D>(@"Collectibles/Level1/star");
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 50, 50);
            if (omReference == null)
            {
                omReference = (ObjectManager)game.Services.GetService(typeof(ObjectManager));
            }
        }

        public override void Update(GameTime gameTime)
        {
            position.Y -= movementPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 50, 50);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(starArt, collisionBox, Color.White);
            base.Draw(gameTime);
        }

        public override void Collision(GameObjectCollidable goc)
        {
            if(goc as Player != null)
            {
                DeleteFlag = true;
                omReference.UnregisterObject(this);
            }
            base.Collision(goc);
        }
    }
}
