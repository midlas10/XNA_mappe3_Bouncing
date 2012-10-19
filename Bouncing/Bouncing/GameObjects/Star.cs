using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bouncing.CollisionSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.GameObjects
{
    public class Star : GameObjectCollidable
    {
        protected SpriteBatch spriteBatch;
        protected Game game;

        public Star(Game baseGame, Vector2 position, SpriteBatch spriteBatchToUse)
            : base(position)
        {
            spriteBatch = spriteBatchToUse;
            game = baseGame;
            collisionBox = new Rectangle((int) position.X, (int) position.Y, 0, 0);
        }
    }
}
