﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bouncing
{
    public class GameOverScreen : MenuScreen
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

        MainMenuEntry submenu, mainmenu, quit;

        public GameOverScreen(GameScreen parent)
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
            submenu = new MainMenuEntry(this, "Select Level");
            submenu.AddSubMenu(new LevelSelectScreen(this));
            mainmenu = new MainMenuEntry(this, "Quit to Main Menu");
            mainmenu.Selected += new EventHandler(MainMenuSelect);
            quit = new MainMenuEntry(this, "Quit Game");
            quit.Selected += new EventHandler(QuitSelect);

            //Finally, add all entries to the list
            //MenuEntries.Add(resume);
            MenuEntries.Add(submenu);
            MenuEntries.Add(mainmenu);
            MenuEntries.Add(quit);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            SpriteFont = content.Load<SpriteFont>("Fonts/MenuFont");

            //Positions the menu entries
            //resume.SetPosition(new Vector2(100, 200), true);

            submenu.SetPosition(new Vector2(100, 200), true);
            mainmenu.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), submenu, true);
            quit.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), mainmenu, true);


            submenu.Selected += new EventHandler(submenu_Selected);

            Title = "Game Over";
            OffsetTitle = new Vector2(400, 0);
        }
        void submenu_Selected(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.RemoveScreen(this);
        }

        void MainMenuSelect(object sender, EventArgs e)
        {
            MenuCancel();
            ExitScreen();
            ScreenSystem.AddScreen(new MainMenuScreen());
            ScreenSystem.RemoveScreen(this);
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
