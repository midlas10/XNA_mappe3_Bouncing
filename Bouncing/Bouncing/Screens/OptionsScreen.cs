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
    public class OptionsScreen : MenuScreen
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

        MainMenuEntry back;

        public OptionsScreen(MenuScreen parent)
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
            Selected = Color.Yellow;
            Highlighted = Color.Green;
            Normal = Color.White;
        }

        public override void Initialize()
        {
            //Fade the screen below
            EnableFade(Color.Black, 0.8f);

            //Keys are already mapped from Menu Screen so we do not need to map
            //them again

            //Initialize the entry and add it to the list.
            back = new MainMenuEntry(this, "Back");
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

            back.SetPosition(new Vector2(100 + push.X, 200), true);
            back.Selected += new EventHandler(back_Selected);

            //The title of the menu entry that activated this menu
            //Will be dynamic in build 0.9x           
            Title = "Options";
            OffsetTitle = new Vector2(-150, 0);
        }

        void back_Selected(object sender, EventArgs e)
        {
            MenuCancel();
            ScreenSystem.AddScreen(new MainMenuScreen());
            ScreenSystem.RemoveScreen(this);
        }
    }
}
