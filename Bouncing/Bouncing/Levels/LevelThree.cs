﻿using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Levels
{
    class LevelThree : BaseLevel
    {

        private Player player;
        private Vortex enemy;
        private SpawnManager spawnManager;

        public override void LoadContent()
        {
            Background tempBack = new Background(TheGame.Content.Load<Texture2D>(@"Maps/Level3/lavalevel"), TheSpriteBatch);


            player = new Player(TheGame, TheSpriteBatch,
                new Vector2(700,
                    500));
            player.LoadContent();

            //Loading the Enemy Sprites
            enemy = new Vortex(TheGame, TheSpriteBatch,
                new Vector2(200,
                    200));
            enemy.LoadContent();

            spawnManager = new SpawnManager(TheGame, TheSpriteBatch);

            //Object Manager
            objectManager.RegisterObject(tempBack);
            objectManager.RegisterObject(enemy);
            objectManager.RegisterObject(player);
            collisionManager.RegisterObject(player);
            collisionManager.RegisterObject(enemy);

            levelLoaded = true;
        }

        public override void UnLoadContent()
        {
            levelLoaded = false;
            player = null;
            enemy = null;
            spawnManager = null;
            base.UnLoadContent();
        }

        public override bool LevelDone()
        {
            if (player.GetTotalStarsCollected() >= 9)
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            spawnManager.Update(gameTime);
        }

        public override bool GameOver()
        {
            return player.IsDead();
        }
    }
}
