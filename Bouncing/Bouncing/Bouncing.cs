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

namespace Bouncing
{
    public class Bouncing : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;

        public Bouncing()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            //base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

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