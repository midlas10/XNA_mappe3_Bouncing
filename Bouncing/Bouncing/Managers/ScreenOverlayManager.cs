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


namespace Bouncing.Managers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenOverlayManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont scoreFont;
        private PlayScreen playScreen;

        public ScreenOverlayManager(Game game, PlayScreen playscreen)
            : base(game)
        {
            this.playScreen = playscreen;
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
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            scoreFont = Game.Content.Load<SpriteFont>("fonts/GameFont");
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // TODO: hente ut score fra playscreen
            spriteBatch.DrawString(scoreFont, "score: " + playScreen.starsCollected, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
