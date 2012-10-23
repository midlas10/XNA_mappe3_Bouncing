using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//Example on how to use the ScreenSystem.  If you wish to use it in your own
//project, grab the ScreenSystem.dll found in the ScreenSystemImplementation
//folder and paste it in your project's folder.  Once that is done, right click
//References on your code project (not your content project) and click Add
//Reference.  Click the browse tab and look for the DLL.
namespace Bouncing
{
    public class MainMenuScreen : MenuScreen
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

        MainMenuEntry play, submenu, intro, quit;

        public MainMenuScreen()
        {
            //Set up the action names
            prevEntry = "MenuUp";
            nextEntry = "MenuDown";
            selectedEntry = "MenuAccept";
            cancelMenu = "MenuCancel";

            //Allow transitions
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //Customize the text colors.
            Selected = Color.Purple;
            Highlighted = Color.Purple;
            Normal = Color.Gray;
        }

        public override void Initialize()
        {
            //Get a reference to the input system
            InputSystem input = ScreenSystem.InputSystem;

            //Load the actions
            input.NewAction(PreviousEntryActionName, Keys.Up);
            input.NewAction(NextEntryActionName, Keys.Down);
            input.NewAction(SelectedEntryActionName, Keys.Enter);
            input.NewAction(MenuCancelActionName, Keys.Escape);

            //Initialize the entries
            play = new MainMenuEntry(this, "Play Screen");
            submenu = new MainMenuEntry(this, "Sub-Menu Entry");
            intro = new MainMenuEntry(this, "Intro Screen");
            quit = new MainMenuEntry(this, "Quit Game");

            //Set up the screen events
            Removing += new EventHandler(MainMenuRemoving);
            Entering += new TransitionEventHandler(MainMenuScreen_Entering);
            Exiting += new TransitionEventHandler(MainMenuScreen_Exiting);

            //Set up the entry events, and load a submenu.
            play.Selected += new EventHandler(PlaySelect);
            submenu.AddSubMenu(new OptionsScreen(this));
            intro.Selected += new EventHandler(IntroSelect);
            quit.Selected += new EventHandler(QuitSelect);

            //Finally, add all entries to the list
            MenuEntries.Add(play);
            MenuEntries.Add(submenu);
            MenuEntries.Add(intro);
            MenuEntries.Add(quit);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenSystem.Content;
            SpriteFont = content.Load<SpriteFont>(@"Fonts/MenuFont");

            play.SetPosition(new Vector2(100, 200), true);
            submenu.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), play, true);
            intro.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), submenu, true);
            quit.SetRelativePosition(new Vector2(0, SpriteFont.LineSpacing + 5), intro, true);
        }

        public override void UnloadContent()
        {
            SpriteFont = null;
        }

        void MainMenuScreen_Entering(object sender, TransitionEventArgs tea)
        {
            //Slide effect from left to right
            float effect = (float)Math.Pow(tea.percent - 1, 2) * -100;
            foreach (MenuEntry entry in MenuEntries)
            {
                entry.Acceleration = new Vector2(effect, 0);
                entry.Position = entry.InitialPosition + entry.Acceleration;
            }
        }

        void MainMenuScreen_Exiting(object sender, TransitionEventArgs tea)
        {
            //Slide effect from right to left
            float effect = (float)Math.Pow(tea.percent - 1, 2) * 100;
            foreach (MenuEntry entry in MenuEntries)
            {
                entry.Acceleration = new Vector2(effect, 0);
                entry.Position = entry.InitialPosition - entry.Acceleration;
            }
        }

        void PlaySelect(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.AddScreen(new PlayScreen());
        }

        void IntroSelect(object sender, EventArgs e)
        {
            ExitScreen();
            ScreenSystem.AddScreen(new IntroScreen(Color.Black, 0.5f));
        }

        void QuitSelect(object sender, EventArgs e)
        {
            ExitScreen();
        }


        void MainMenuRemoving(object sender, EventArgs e)
        {
            MenuEntries.Clear();
        }
    }
}
