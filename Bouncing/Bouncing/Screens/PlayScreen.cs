using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        SpriteBatch spriteBatch;
        InputManager _input;
        ScreenSystem screenSystem;
        Color clearColor;

        private ObjectManager objectManager;
        private IManageCollisionsService collisionManager;

        private Player player;
        private Enemy enemy;

        public PlayScreen()
        {
            clearColor = new Color(70, 132, 143);
            objectManager = (ObjectManager) ScreenSystem.Game.Services.GetService(typeof (GameObject));
            collisionManager = (IManageCollisionsService)ScreenSystem.Game.Services.GetService((typeof(IManageCollisionsService)));

        }

        public override void Initialize()
        {
            //objectManager = new ObjectManager(this);

            input = ScreenSystem.InputSystem;
            input.NewAction("Pause", Keys.Escape);

            Entering += new TransitionEventHandler(PlayScreen_Entering);
        }

        void PlayScreen_Entering(object sender, TransitionEventArgs tea)
        {
            titleColor = Color.Green * TransitionPercent;
            descriptionColor = Color.White * TransitionPercent;
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics);
            objectManager.SetSpritebatch(spriteBatch);

            Background tempBack = new Background(ScreenSystem.Game.Content.Load<Texture2D>(@"Maps/Level1/space"), spriteBatch);


            player = new Player(ScreenSystem.Game, spriteBatch,
                new Vector2(300,
                    300));
            player.LoadContent();


            objectManager.RegisterObject(player);
            collisionManager.RegisterObject(player);


            //Loading the Enemy Sprites
            enemy = new Enemy(ScreenSystem.Game, spriteBatch,
                new Vector2(200,
                    200));
            enemy.LoadContent();


            //Loading the Collectibles
            Star test = new Star(screenSystem.Game,
                new Vector2(600,
                    200), spriteBatch);

            //Object Manager
            objectManager.RegisterObject(test);
            objectManager.RegisterObject(enemy);
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
        

            _input.Update(gameTime);
            //if (_input.IsKeyDown(Keys.Escape))
            //    this.Exit();

            //AudioManager.singleton.Update();


            collisionManager.Update(gameTime);
            objectManager.Update(gameTime);


            base.Update(gameTime);
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
            ScreenSystem.Game.GraphicsDevice.Clear(clearColor);
            objectManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
