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

        MainMenuEntry back, lavaLevel;


        public LevelSelectScreen(MenuScreen parent)
        {
            //Like the pause screen, we will reset the parent to active
            //when this screen is finished.  Therefore, we must have a reference
            //to the parent
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

            lavaLevel = new MainMenuEntry(this, "Lava world");
            back = new MainMenuEntry(this, "Back");

            MenuEntries.Add(back);
            MenuEntries.Add(lavaLevel);
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

            
            back.SetPosition(new Vector2(100 + push.X, 200), true);
            lavaLevel.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), back, true);
            back.Selected += new EventHandler(back_Selected);

            lavaLevel.Selected += new EventHandler(lavaLevel_Selected);

            //The title of the menu entry that activated this menu
            //Will be dynamic in build 0.9x           
            Title = "Select Level";
            OffsetTitle = new Vector2(-150, 0);
        }

        void back_Selected(object sender, EventArgs e)
        {
            MenuCancel();
        }

        void lavaLevel_Selected(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.AddScreen(new PlayScreen(1));
        }

    }
}
