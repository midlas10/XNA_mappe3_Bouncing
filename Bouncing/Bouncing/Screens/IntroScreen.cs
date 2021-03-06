﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Bouncing
{
    public class IntroScreen : LogoScreen
    {
        public IntroScreen()
            : base() { }

        public IntroScreen(Color fadeColor, float fadePercent)
            : base(fadeColor, fadePercent) { }

        public override void Initialize()
        {
            ScreenTime = TimeSpan.FromSeconds(0);
            Removing += new EventHandler(RemovingScreen);
            base.Initialize();
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            Texture = content.Load<Texture2D>("Images/intro");
        }

        public override void UnloadContent()
        {
            Texture = null;
        }

        void RemovingScreen(object sender, EventArgs e)
        {
            //Loads the MainMenuScreen when Intro is over
            ScreenSystem.AddScreen(new MainMenuScreen());
            ScreenSystem.RemoveScreen(this);
        }
    }
}
