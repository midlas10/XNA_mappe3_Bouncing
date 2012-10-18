using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bouncing.CollisionSystem;
using Bouncing.GameObjects;
using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Levels
{
    class BaseLevel : ILevel
    {
        protected Game TheGame;
        protected SpriteBatch TheSpriteBatch;
        protected ObjectManager objectManager;
        protected IManageCollisionsService collisionManager;
        public void Init(Game game, SpriteBatch spriteBatchToUse)
        {
            TheSpriteBatch = spriteBatchToUse;
            TheGame = game;
            objectManager = (ObjectManager)TheGame.Services.GetService(typeof(ObjectManager));
            collisionManager =
                (IManageCollisionsService)game.Services.GetService((typeof(IManageCollisionsService)));
        }

        public virtual void LoadContent()
        {
            
        }

        public virtual void UnLoadContent()
        {

        }

        public virtual bool LevelDone()
        {
            return false;
        }

        public virtual bool GameOver()
        {
            return false;
        }
    }
}
