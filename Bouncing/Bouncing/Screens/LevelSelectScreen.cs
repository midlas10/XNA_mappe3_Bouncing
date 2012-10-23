using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Bouncing
{
    public class LevelSelectScreen : MenuScreen
    {
        string prevEntry, nextEntry, selectedEntry, cancelMenu;
        public override string PreviousEntryActionName
        {
            get { return prevEntry; }
        }

        public override string NextEntryActionName
        {
            get { return nextEntry; }
        }

        public override string SelectedEntryActionName
        {
            get { return selectedEntry; }
        }

        public override string MenuCancelActionName
        {
            get { return cancelMenu; }
        }

        MainMenuEntry back, 
            spaceLevel,
            lavaLevel, 
            underwater;


        public LevelSelectScreen(MenuScreen parent)
        {
            this.Parent = parent;

            //Load the action titles
            prevEntry = "MenuUp";
            nextEntry = "MenuDown";
            selectedEntry = "MenuAccept";
            cancelMenu = "MenuCancel";

            //Customize the text colors.
            Selected = Color.Purple;
            Highlighted = Color.Purple;
            Normal = Color.Gray;
        }

        public override void Initialize()
        {
            //Fade the screen below
            EnableFade(Color.Black, 0.8f);

            //Keys are already mapped from Menu Screen so we do not need to map
            //them again

            //Initialize the entry and add it to the list.

            spaceLevel = new MainMenuEntry(this, "Space Vortex");
            underwater = new MainMenuEntry(this, "Bubbly Waters");
            lavaLevel = new MainMenuEntry(this, "Molten Fields");
            back = new MainMenuEntry(this, "Back");

            MenuEntries.Add(spaceLevel);
            MenuEntries.Add(underwater);
            MenuEntries.Add(lavaLevel);
            MenuEntries.Add(back);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            SpriteFont = content.Load<SpriteFont>("Fonts/MenuFont");

            //Code to push the title of the menu
            //Will be dynamic in a few builds
            Vector2 push = Vector2.Zero;
            if (Parent != null)
                push = ((MenuScreen)Parent).MenuEntries[0].Position +
                    ((MenuScreen)Parent).SpriteFont.MeasureString(((MenuScreen)Parent).MenuEntries[0].EntryTitle);

            
            

            spaceLevel.SetPosition(new Vector2(100 + push.X, 200), true);
            underwater.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), spaceLevel, true);
            lavaLevel.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), underwater, true);
            back.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), lavaLevel, true);
            
            
            spaceLevel.Selected += new EventHandler(spaceLevel_Selected);
            underwater.Selected += new EventHandler(underwater_Selected);
            lavaLevel.Selected += new EventHandler(lavaLevel_Selected);
            back.Selected += new EventHandler(back_Selected);


            Title = "Select Level";
            OffsetTitle = new Vector2(-150, 0);
        }

        void back_Selected(object sender, EventArgs e)
        {
            MenuCancel();
        }

        void spaceLevel_Selected(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.AddScreen(new PlayScreen(1));
        }
        void underwater_Selected(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.AddScreen(new PlayScreen(2));
        }
        void lavaLevel_Selected(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.AddScreen(new PlayScreen(3));
        }
        
        
    }
}
