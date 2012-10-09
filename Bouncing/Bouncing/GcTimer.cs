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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GcTimer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch sb;
        SpriteFont font1;
        int fps;
        int frameCounter;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public GcTimer(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font1 = Game.Content.Load<SpriteFont>("SpriteFont1");
            sb = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //Console.WriteLine("Gc!");
            
            // Legg til tiden som har gått siden forrige update til telleren
            elapsedTime += gameTime.ElapsedGameTime;

            // Hvis tiden det har gått siden forrige reset av elapsedTime er over 1 sekund
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                //Ta vekk et sekund fra tidstelleren
                elapsedTime -= TimeSpan.FromSeconds(1);

                fps = frameCounter;

                //reset resten
                frameCounter = 0;

            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            frameCounter++;
            sb.Begin();
            sb.DrawString(font1, "FPS: " + fps, new Vector2(1,1), Color.Black);
            sb.DrawString(font1, "FPS: " + fps, Vector2.Zero, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
