/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace ScreenSystemImplementation
{
    /// <summary>
    /// Sample play screen.  No new features are presented here,
    /// so there are no comments currently.
    /// </summary>
    public class PlayScreen : GameScreen
    {
        public override bool AcceptsInput
        {
            get { return true; }
        }

        string title, description;

        float seconds;

        Color titleColor, descriptionColor;

        SpriteFont font;

        Vector2 position;

        InputSystem input;

        public override void Initialize()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            title = "-----Play Screen-----";
            titleColor = Color.Green;
            description = "You put your play logic in this screen.  Press Escape to pause";
            descriptionColor = Color.White;

            position = new Vector2(100, 200);

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
            ContentManager content = ScreenSystem.Content;
            font = content.Load<SpriteFont>("gamefont");
        }

        public override void UnloadContent()
        {
            font = null;
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            position = new Vector2(100, 200);
            SpriteBatch spriteBatch = ScreenSystem.SpriteBatch;
            spriteBatch.DrawString(font, title, position, titleColor);
            position = Vector2.Add(position, new Vector2(0, font.LineSpacing + 10));
            spriteBatch.DrawString(font, description, position, descriptionColor);
            position = Vector2.Add(position, new Vector2(0, font.LineSpacing + 10));
            spriteBatch.DrawString(font, ((int)seconds).ToString(), position, Color.White);
        }
    }
}
*/