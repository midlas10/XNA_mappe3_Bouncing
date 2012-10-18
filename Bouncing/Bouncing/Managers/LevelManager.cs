﻿using System.Collections.Generic;
using Bouncing.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing.Managers
{
    class LevelManager : IManageLevels
    {
        protected List<ILevel> Levels;
        protected Game TheGame;
        protected ILevel CurLevel;
        protected int CurLevelNR;
        protected SpriteBatch TheSpriteBatch;

        public void Init(Game theGame, SpriteBatch spriteBatchToUse)
        {
            Levels = new List<ILevel>();
            TheGame = theGame;
            TheSpriteBatch = spriteBatchToUse;
            CurLevelNR = 0;

            ILevel levelOne = new LevelOne();
            Levels.Add(levelOne);
        }

        public ILevel NextLevel()
        {
            if (Levels.Count <= (CurLevelNR + 1))
            {
                ILevel temp = Levels[CurLevelNR];
                temp.Init(TheGame, TheSpriteBatch);
                CurLevelNR++;
                return temp;
            }
            return null;
        }

        public ILevel PrevLevel()
        {
            if ((CurLevelNR - 1) >= 0)
            {
                --CurLevelNR;
                return Levels[CurLevelNR];
            }
            return null;
        }

        public ILevel GetLevel(int levelID)
        {
            if (Levels.Count >= levelID)
            {
                --CurLevelNR;
                return Levels[CurLevelNR];
            }

            return null;
        }
    }
}