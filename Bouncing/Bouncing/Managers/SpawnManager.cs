using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bouncing.CollisionSystem;
using Bouncing.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Managers
{
    class SpawnManager
    {
        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        protected Game game;
        protected Random random = new Random();
        protected double nextTime;
        protected double sinceLastSendt;
        protected SpriteBatch spriteBatch;

        SpawnManager(Game game, SpriteBatch batch)
        {
            this.game = game;
            spriteBatch = batch;
            objectManager = (ObjectManager) game.Services.GetService((typeof(GameObject)));
            collisionManager = (IManageCollisionsService)game.Services.GetService((typeof(IManageCollisionsService)));
            nextTime = random.Next(0, 10);
        }

        public void Update(GameTime gameTime)
        {
            sinceLastSendt += gameTime.ElapsedGameTime.TotalSeconds;
            if(sinceLastSendt >= nextTime)
            {
                int startXPos = random.Next(0, game.Window.ClientBounds.Width);
                Star temp = new Star(game, new Vector2(startXPos, 0), spriteBatch);
                objectManager.RegisterObject(temp);
                collisionManager.RegisterObject(temp);
                sinceLastSendt = 0;
                nextTime = random.Next(0, 10);
            }
        }
    }
}
