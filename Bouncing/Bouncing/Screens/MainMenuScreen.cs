using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenSystemLibrary;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


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
            //Sets up names
            prevEntry = "MenuUp";
            nextEntry = "MenuDown";
            selectedEntry = "MenuAccept";
            cancelMenu = "MenuCancel";

            //Allow transitions
            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //Customize the text colors.
            Selected = Color.Yellow;
            Highlighted = Color.Green;
            Normal = Color.White;
        }

        public override void Initialize()
        {
            //Get a reference to the input system
            InputSystem input = ScreenSystem.InputSystem;

            //Load the actions and user input
            input.NewAction(PreviousEntryActionName, Keys.Up);
            input.NewAction(NextEntryActionName, Keys.Down);
            input.NewAction(SelectedEntryActionName, Keys.Enter);
            input.NewAction(MenuCancelActionName, Keys.Escape);

            //Initialize the entries
            play = new MainMenuEntry(this, "Play");
            submenu = new MainMenuEntry(this, "Options");
            intro = new MainMenuEntry(this, "Intro");
            quit = new MainMenuEntry(this, "Quit");

            //Set up the screen events
            Removing += new EventHandler(MainMenuRemoving);
            Entering += new TransitionEventHandler(MainMenuScreen_Entering);
            Exiting += new TransitionEventHandler(MainMenuScreen_Exiting);

            //Set up the entry events, and load a submenu.
            play.Selected += new EventHandler(PlaySelect);
            //submenu.AddSubMenu(new SubMenu(this));
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

            //Initialize is called before LoadContent, so if you want to 
            //use relative position with the line spacing like below,
            //you need to do it after load content and spritefont
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
            //ScreenSystem.AddScreen(new PlayScreen());
        }

        void IntroSelect(object sender, EventArgs e)
        {
            ExitScreen();
            //ScreenSystem.AddScreen(new PHSLogoScreen(Color.Black, 0.5f));
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
