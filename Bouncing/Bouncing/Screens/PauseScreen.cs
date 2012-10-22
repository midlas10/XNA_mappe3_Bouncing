using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bouncing
{
    public class PauseScreen : MenuScreen
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

        MainMenuEntry resume, submenu, mainmenu, quit;

        public PauseScreen(GameScreen parent)
        {
            //Set up the parent to resume when pause screen is done
            this.Parent = parent;

            //Set up the action names
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

            //Initialize the entries and set up the events
            resume = new MainMenuEntry(this, "Resume");
            resume.Selected += new EventHandler(ResumeSelect);
            submenu = new MainMenuEntry(this, "Options");
            submenu.AddSubMenu(new OptionsScreen(this));
            mainmenu = new MainMenuEntry(this, "Quit to Main Menu");
            mainmenu.Selected += new EventHandler(MainMenuSelect);
            quit = new MainMenuEntry(this, "Quit Game");
            quit.Selected += new EventHandler(QuitSelect);

            //Finally, add all entries to the list
            MenuEntries.Add(resume);
            MenuEntries.Add(submenu);
            MenuEntries.Add(mainmenu);
            MenuEntries.Add(quit);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            SpriteFont = content.Load<SpriteFont>("Fonts/MenuFont");

            //Positions the menu entries
            resume.SetPosition(new Vector2(100, 200), true);
            submenu.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), resume, true);
            mainmenu.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), submenu, true);
            quit.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), mainmenu, true);
        }

        void MainMenuSelect(object sender, EventArgs e)
        {
            MenuCancel();
            Parent.ExitScreen();
            ScreenSystem.AddScreen(new MainMenuScreen());
        }

        void ResumeSelect(object sender, EventArgs e)
        {
            MenuCancel();
        }

        void QuitSelect(object sender, EventArgs e)
        {
            ScreenSystem.Game.Exit();
        }
    }
}
