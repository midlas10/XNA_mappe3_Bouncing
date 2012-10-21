using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;


namespace Bouncing
{
    public class MainMenuEntry : MenuEntry
    {
        public MainMenuEntry(MenuScreen menu, string title)
            : base(menu, title)
        {

        }

        public override void AnimateHighlighted(GameTime gameTime)
        {
            
            float pulseEffect = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3) + 1);
            Scale = 1 + pulseEffect * 0.05f;


        }

        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(InitialPosition.X, InitialPosition.Y);
        }
    }
}
