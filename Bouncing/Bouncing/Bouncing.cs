using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ScreenSystemLibrary;

namespace Bouncing
{
    
    public class Bouncing : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ScreenSystem screenSystem;
        InputManager _input;

        Color clearColor;

        private ObjectManager objectManager;
        private IManageCollisionsService collisionDetectionService;


        public Bouncing()
        {
            clearColor = new Color(22,22,22);
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";

            screenSystem = new ScreenSystem(this);
            Components.Add(screenSystem);

            objectManager = new ObjectManager(this);
            collisionDetectionService = new CollisionDetectionCircleService(this);

            _input = new InputManager(this);
            
            Services.AddService(typeof(ObjectManager), objectManager);
            Services.AddService(typeof(IManageCollisionsService), collisionDetectionService);
            Services.AddService(typeof(IInputService), _input);
        }
        

        protected override void Initialize()
        {
            Settings.MusicVolume = 1.0f;
            Settings.MusicVolume = 1.0f;

            // TODO: Add your initialization logic here
            //screenSystem.AddScreen(new IntroScreen(Color.Black, 0.5f));
            screenSystem.AddScreen(new MainMenuScreen());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            objectManager.SetSpritebatch(screenSystem.SpriteBatch);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            _input.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);


            base.Draw(gameTime);
        }
    }
}
