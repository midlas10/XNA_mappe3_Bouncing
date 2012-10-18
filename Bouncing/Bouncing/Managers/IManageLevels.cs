using Bouncing.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Managers
{
    public interface IManageLevels
    {
        void Init(Game theGame, SpriteBatch spriteBatchToUse);
        ILevel NextLevel();
        ILevel PrevLevel();
        ILevel GetLevel(int levelID);
    }
}
