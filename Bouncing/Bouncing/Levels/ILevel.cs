using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Levels
{
   public interface ILevel
   {
        void Init(Game game, SpriteBatch spriteBatchToUse);
        void LoadContent();
        void UnLoadContent();
        bool LevelDone();
        bool GameOver();
    }
}
