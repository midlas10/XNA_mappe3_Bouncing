using Bouncing.GameObjects;
using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bouncing.Managers;

namespace Bouncing.Levels
{
    class LevelOne : BaseLevel
    {

        public override void LoadContent()
        {
            Player tempPlayer = new Player(TheGame, TheSpriteBatch, new Vector2(100, 100));
            tempPlayer.LoadContent();
            objectManager.RegisterObject(tempPlayer);
            collisionManager.RegisterObject(tempPlayer);
        }

        public override void UnLoadContent()
        {
            
        }

        public override bool LevelDone()
        {
            return false;
        }

        public override bool GameOver()
        {
            return false;
        }
    }
}
