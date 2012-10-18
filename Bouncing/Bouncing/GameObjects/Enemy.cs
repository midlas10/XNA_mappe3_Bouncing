using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.GameObjects
{
    public class Enemy : CollisionSystem.GameObjectCollidable
    {
        protected Texture2D art;
        protected Game game;

        protected float rotation;
        protected SpriteBatch spriteBatch;


        protected ObjectManager om;

        public Enemy(Game baseGame, Vector2 position, SpriteBatch spriteBatchToUse)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

    }

}
