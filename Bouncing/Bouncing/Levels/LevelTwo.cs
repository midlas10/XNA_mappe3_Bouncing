using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Levels
{
    class LevelTwo : BaseLevel
    {

        private Player player;
        private Enemy enemy;
        private SpawnManager spawnManager;

        public override void LoadContent()
        {
            Background tempBack = new Background(TheGame.Content.Load<Texture2D>(@"Maps/Level2/underwaterlvl"), TheSpriteBatch);


            player = new Player(TheGame, TheSpriteBatch,
                new Vector2(300,
                    300));
            player.LoadContent();

            //Loading the Enemy Sprites
            enemy = new Enemy(TheGame, TheSpriteBatch,
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
            base.UnLoadContent();
        }

        public override bool LevelDone()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            spawnManager.Update(gameTime);
        }

        public override bool GameOver()
        {
            return false;
        }
    }
}
