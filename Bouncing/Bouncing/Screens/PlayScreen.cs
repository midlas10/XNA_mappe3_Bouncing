using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bouncing.Levels;
using Bouncing.Managers;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Bouncing
{
    
    public class PlayScreen : GameScreen
    {
        public override bool AcceptsInput
        {
            get { return true; }
        }

        float seconds;
        Color titleColor, descriptionColor;
        SpriteFont font;
        InputSystem input;
        string songTitle;

        GraphicsDevice graphics;
        InputManager _input;
        ScreenSystem screenSystem;

        private ScreenOverlayManager screenOverlayManager;
        private ObjectManager objectManager;
        private IManageCollisionsService collisionManager;

        private ILevel curLevel;
        private int level;
        private LevelManager levelManager;
        private AudioManager audioManager;

        public int starsCollected { get; set; }

        public PlayScreen()
        {
            level = 1;
        }

        public PlayScreen(int level)
        {
            this.level = level;

        }

        public override void Initialize()
        {
            starsCollected = 1;
            levelManager = new LevelManager();
            levelManager.Init(ScreenSystem.Game, ScreenSystem.SpriteBatch);
            if (level == 0)
            {
                curLevel = levelManager.NextLevel();
            }
            else
            {
                curLevel = levelManager.GetLevel(level);
            }


            //Inits the audio
            switch (level)
            {
                case 1:
                    audioManager = new AudioManager("spacetheme");
                    break;
                case 2:
                    audioManager = new AudioManager("spacetheme");
                    break;
                case 3:
                    audioManager = new AudioManager("lavatheme");
                    break;
            }

            audioManager.LoadContent();


            objectManager = (ObjectManager)ScreenSystem.Game.Services.GetService((typeof(ObjectManager)));
            collisionManager = (IManageCollisionsService)ScreenSystem.Game.Services.GetService((typeof(IManageCollisionsService)));
            _input = (InputManager)ScreenSystem.Game.Services.GetService(typeof(IInputService));
            input = ScreenSystem.InputSystem;
            input.NewAction("Pause", Keys.Escape);
            screenOverlayManager = new ScreenOverlayManager(ScreenSystem.Game, this);
            ScreenSystem.Game.Components.Add(screenOverlayManager);
            Entering += new TransitionEventHandler(PlayScreen_Entering);
        }

        void PlayScreen_Entering(object sender, TransitionEventArgs tea)
        {
            titleColor = Color.Green * TransitionPercent;
            descriptionColor = Color.White * TransitionPercent;
        }

        public override void LoadContent()
        {
            
            base.LoadContent();

            ContentManager content = ScreenSystem.Content;
            font = content.Load<SpriteFont>("Fonts/GameFont");
        }

        public override void UnloadContent()
        {
            font = null;
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
       
            if (curLevel != null)
            {
                if (!curLevel.IsLevelLoaded())
                {
                    curLevel.LoadContent();
                }

                collisionManager.Update(gameTime);
                objectManager.Update(gameTime);
                curLevel.Update(gameTime);

                if (curLevel.LevelDone())
                {
                    curLevel.UnLoadContent();
                    curLevel = levelManager.NextLevel();
                } 
                
                else if (curLevel.GameOver())
                {
                    ExitScreen();
                    ScreenSystem.AddScreen(new GameOverScreen(this));
                }

            } else 
            {
                ExitScreen();
                ScreenSystem.AddScreen(new MainMenuScreen());
            }
        }

        public override void HandleInput()
        {
            if (input.NewActionPress("Pause"))
            {
                FreezeScreen();
                ScreenSystem.AddScreen(new PauseScreen(this));
            }
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            
            objectManager.Draw(gameTime);

        }
    }
}
