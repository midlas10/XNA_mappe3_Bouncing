using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        GraphicsDevice graphics;
        InputManager _input;
        ScreenSystem screenSystem;

        private SpawnManager spawnManager;

        private ObjectManager objectManager;
        private IManageCollisionsService collisionManager;

        private Player player;
        private Enemy enemy;

        public PlayScreen()
        {

        }

        public override void Initialize()
        {
            objectManager = (ObjectManager)ScreenSystem.Game.Services.GetService((typeof(ObjectManager)));
            collisionManager = (IManageCollisionsService)ScreenSystem.Game.Services.GetService((typeof(IManageCollisionsService)));
            _input = (InputManager)ScreenSystem.Game.Services.GetService(typeof(IInputService));
            input = ScreenSystem.InputSystem;
            input.NewAction("Pause", Keys.Escape);
            spawnManager = new SpawnManager(ScreenSystem.Game, ScreenSystem.SpriteBatch);
            Entering += new TransitionEventHandler(PlayScreen_Entering);
        }

        void PlayScreen_Entering(object sender, TransitionEventArgs tea)
        {
            titleColor = Color.Green * TransitionPercent;
            descriptionColor = Color.White * TransitionPercent;
        }

        public override void LoadContent()
        {
            
            Background tempBack = new Background(ScreenSystem.Game.Content.Load<Texture2D>(@"Maps/Level1/space"), ScreenSystem.SpriteBatch);


            player = new Player(ScreenSystem.Game, ScreenSystem.SpriteBatch,
                new Vector2(300,
                    300));
            player.LoadContent();

            //Loading the Enemy Sprites
            enemy = new Enemy(ScreenSystem.Game, ScreenSystem.SpriteBatch,
                new Vector2(200,
                    200));
            enemy.LoadContent();


            //Loading the Collectibles
            Star test = new Star(ScreenSystem.Game,
                new Vector2(600,
                    200), ScreenSystem.SpriteBatch);

            //Object Manager
            objectManager.RegisterObject(tempBack);
            objectManager.RegisterObject(test);
            objectManager.RegisterObject(enemy);
            objectManager.RegisterObject(player);
            collisionManager.RegisterObject(player);
            collisionManager.RegisterObject(enemy);
            collisionManager.RegisterObject(test);
            
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
       
            //if (_input.IsKeyDown(Keys.Escape))
            //    this.Exit();

            //AudioManager.singleton.Update();

            spawnManager.Update(gameTime);
            collisionManager.Update(gameTime);
            objectManager.Update(gameTime);


            //base.Update(gameTime);
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
            
            //base.Draw(gameTime);
        }
    }
}
