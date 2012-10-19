using Bouncing.CollisionSystem;
using Bouncing.GameObjects;
using Bouncing.Input;
using Bouncing.Levels;
using Bouncing.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using ScreenSystemLibrary;
using Bouncing.Managers;

namespace Bouncing
{
    public class Bouncing : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager _input;
        ScreenSystem screenSystem;
        Color clearColor;

        private ObjectManager objectManager;
        private CollisionDetectionService collisionDetectionService;

        private ILevel curLevel;
        private IManageLevels levelManager;


        private Player player;
        private Enemy enemy;

       
        public Bouncing()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            
            Content.RootDirectory = "Content";


            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 900;

            screenSystem = new ScreenSystem(this);
            Components.Add(screenSystem);

            //Bouncing intro = new Bouncing(Content, "Intro\\");


            IsMouseVisible = false;
            objectManager = new ObjectManager(this);
            collisionDetectionService = new CollisionDetectionService(this);

            levelManager = new LevelManager();
            
            _input = new InputManager(this);
            Components.Add(_input);
            Components.Add(objectManager);
            Services.AddService(typeof(ObjectManager), objectManager);
            Services.AddService(typeof(IManageCollisionsService), collisionDetectionService);
            Services.AddService(typeof(IInputService), _input);
        }

        protected override void Initialize()
        {
            screenSystem.AddScreen(new IntroScreen());
            clearColor = new Color(70, 132, 143);

            Settings.MusicVolume = 1.0f;
            Settings.MusicVolume = 1.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            objectManager.SetSpritebatch(spriteBatch);

            base.LoadContent();
            levelManager.Init(this, spriteBatch);
            curLevel = levelManager.NextLevel();
            curLevel.LoadContent();


            /*
            AudioEngine audio = new AudioEngine(Content.RootDirectory + "//Audio//gameIntro.xgs");
            WaveBank waveBank = new WaveBank(audio, Content.RootDirectory + "//Audio//Wave bank.xwb");
            SoundBank soundBank = new SoundBank(audio, Content.RootDirectory + "//Audio//Sound Bank.xsb");

            AudioManager manager = new AudioManager(audio, waveBank, soundBank);
            */

            player = new Player(this, spriteBatch, 
                new Vector2(graphics.PreferredBackBufferWidth / 2, 
                    graphics.PreferredBackBufferHeight / 2));
            player.LoadContent();

            objectManager.RegisterObject(player);
            collisionDetectionService.RegisterObject(player);

            enemy = new Enemy(this, spriteBatch,
                new Vector2(graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2));
            enemy.LoadContent();

            objectManager.RegisterObject(enemy);
            collisionDetectionService.RegisterObject(enemy);

            base.LoadContent();

        }

        protected override void UnloadContent()
        {
            //base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update(gameTime);
            if (_input.IsKeyDown(Keys.Escape))
                this.Exit();

            //AudioManager.singleton.Update();

            
            collisionDetectionService.Update(gameTime);
            objectManager.Update(gameTime);

            if(curLevel.LevelDone())
            {
                curLevel.UnLoadContent();
                curLevel = levelManager.NextLevel();
                if(curLevel == null)
                {
                    
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);

            base.Draw(gameTime);
        }
    }
}

/*
namespace Bouncing
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tullTexture;
        BouncingObject tull, tull2;
        GcTimer gcTimer;
        enum GameState { Start, InGame, GameOver, Pause };
        GameState currentGameState = GameState.Start;
        SpriteFont font1;
        KeyboardState kbState;
        int score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gcTimer = new GcTimer(this);
            Components.Add(gcTimer);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tullTexture = Content.Load<Texture2D>("Tull");
            tull = new BouncingObject(tullTexture, new Point(100, 100), new Vector2(50, 50), new Vector2(5, 5), Window.ClientBounds);
            tull2 = new BouncingObject(tullTexture, new Point(100, 100), new Vector2(100, 70), new Vector2(1, 3), Window.ClientBounds);
            font1 = Content.Load<SpriteFont>("SpriteFont1");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kbState = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.Start:
                    if (kbState.IsKeyDown(Keys.Space))
                    {
                        currentGameState = GameState.InGame;
                    }
                    break;

                case GameState.InGame:
                    if (kbState.IsKeyDown(Keys.Escape))
                    {
                        currentGameState = GameState.Pause;
                    }

                    tull.velocityModifier = 0;

                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        tull.position.Y -= 5f;
                    }

                    if (kbState.IsKeyDown(Keys.Down))
                    {
                        tull.position.Y += 5f;
                    }

                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        tull.position.X -= 5f;
                    }

                    if (kbState.IsKeyDown(Keys.Right))
                    {
                        tull.position.X += 5f;
                    }

                    score += (int) (tull.velocityModifier * 10);

                    tull.Update();
                    tull2.Update();
                    if (tull.collisionBox.Intersects(tull2.collisionBox))
                    {
                        Console.WriteLine("Collision!");
                    }

                    break;

                case GameState.GameOver:
                    break;

                case GameState.Pause:
                    if (kbState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.InGame;
                    }
                    break;

            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.Start:
                    GraphicsDevice.Clear(Color.White);
                    string splashText = "M E G A D U L L: GAME OF THE YEAR EDITION";
                    spriteBatch.DrawString(font1, splashText, new Vector2((Window.ClientBounds.Width / 2) - (font1.MeasureString(splashText).X / 2), 100), Color.Blue);
                    break;

                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Draw(tull2.texture, tull2.position, Color.Red);
                    spriteBatch.Draw(tull.texture, tull.position, Color.White);
                    spriteBatch.DrawString(font1, "" + score, new Vector2(10, 10), Color.White);
                    break;

                case GameState.GameOver:
                    break;

                case GameState.Pause:

                    string pauseText = "Pause. Press ENTER to continue";
                    spriteBatch.DrawString(font1, pauseText, new Vector2((Window.ClientBounds.Width / 2) - (font1.MeasureString(pauseText).X / 2), 100), Color.Blue);
                    break;

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public class Player
    {
        public Texture2D texture;
        public Point size;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle bounds;
        bool hasHitWall = false;
        public float velocityModifier = 1f;
        public Rectangle collisionBox { get; set; }

        public Player(Texture2D texture, Point size, Vector2 startPos, Vector2 startVelocity, Rectangle bounds)
        {
            this.texture = texture;
            this.size = size;
            this.position = startPos;
            this.velocity = startVelocity;
            this.bounds = bounds;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
            Console.WriteLine(collisionBox.ToString());
        }

        public void Update()
        {
            hasHitWall = false;

            position.X += velocity.X * velocityModifier;
            position.Y += velocity.Y * velocityModifier;

            collisionBox = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);

            if (position.X <= 0 || position.X >= bounds.Width - size.X)
            {
                velocity.X = -velocity.X;
                hasHitWall = true;
            }

            if (position.Y <= 0 || position.Y >= bounds.Height - size.Y)
            {
                velocity.Y = -velocity.Y;
                hasHitWall = true;
            }

            if (hasHitWall)
            {
                Console.WriteLine("Hit");
                //velocityModifier += .05f;
                //Stas hvis den multipliserer seg hver gang den treffer veggen.
            }
        }

    }

    public class BouncingObject
    {
        public Texture2D texture;
        public Point size;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle bounds;
        bool hasHitWall = false;
        public float velocityModifier = 1f;
        public Rectangle collisionBox { get; set; }

        public BouncingObject(Texture2D texture, Point size, Vector2 startPos, Vector2 startVelocity, Rectangle bounds)
        {
            this.texture = texture;
            this.size = size;
            this.position = startPos;
            this.velocity = startVelocity;
            this.bounds = bounds;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
            Console.WriteLine(collisionBox.ToString());
        }

        public void Update()
        {
            hasHitWall = false;

            position.X += velocity.X * velocityModifier;
            position.Y += velocity.Y * velocityModifier;

            collisionBox = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);

            if (position.X <= 0 || position.X >= bounds.Width - size.X)
            {
                velocity.X = -velocity.X;
                hasHitWall = true;
            }

            if (position.Y <= 0 || position.Y >= bounds.Height - size.Y)
            {
                velocity.Y = -velocity.Y;
                hasHitWall = true;
            }

            if (hasHitWall)
            {
                Console.WriteLine("Hit");
                //velocityModifier += .05f;
                //Stas hvis den multipliserer seg hver gang den treffer veggen.
            }
        }

    }
}
*/