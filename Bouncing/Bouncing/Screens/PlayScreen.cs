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
        private CollisionDetectionService collisionDetectionService;

        private Player player;
        private Enemy enemy;

        public PlayScreen()
        {
            objectManager = new ObjectManager(this);
            collisionDetectionService = new CollisionDetectionService(this);
            clearColor = new Color(70, 132, 143);


            //levelManager = new LevelManager();

            _input = new InputManager(this);
            Components.Add(_input);
            Components.Add(objectManager);
            Services.AddService(typeof(ObjectManager), objectManager);
            Services.AddService(typeof(IManageCollisionsService), collisionDetectionService);
            Services.AddService(typeof(IInputService), _input);
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
            ContentManager content = 
            objectManager.SetSpritebatch(spriteBatch);

            Background tempBack = new Background(content.Load<Texture2D>(@"Maps/Level1/space"), spriteBatch);


            player = new Player(this, spriteBatch,
                new Vector2(graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2));
            player.LoadContent();


            objectManager.RegisterObject(player);
            collisionDetectionService.RegisterObject(player);


            //Loading the Enemy Sprites
            enemy = new Enemy(this, spriteBatch,
                new Vector2(graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2));
            enemy.LoadContent();


            //Loading the Collectibles
            Star test = new Star(this,
                new Vector2(graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2), spriteBatch);

            //Object Manager
            objectManager.RegisterObject(test);
            objectManager.RegisterObject(enemy);
            collisionDetectionService.RegisterObject(enemy);
            collisionDetectionService.RegisterObject(test);
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
        }

        protected override void Update(GameTime gameTime)
        {

            _input.Update(gameTime);
            //if (_input.IsKeyDown(Keys.Escape))
            //    this.Exit();

            //AudioManager.singleton.Update();


            collisionDetectionService.Update(gameTime);
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);

            base.Draw(gameTime);
        }
    }
}
