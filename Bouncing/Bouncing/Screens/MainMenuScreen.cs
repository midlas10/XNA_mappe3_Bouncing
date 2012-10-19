using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ScreenSystemLibrary;

namespace Bouncing
{
    public class MainMenuScreen : MenuScreen
    {
        string prevEntry, nextEntry, selectedEntry, cancelMenu;
        
        public override void InitializeScreen()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override string MenuCancelActionName
        {
            get { return cancelMenu; }
        }

        public override string NextEntryActionName
        {
            get { return nextEntry; }
        }

        public override string PreviousEntryActionName
        {
            get { return prevEntry; }
        }

        public override string SelectedEntryActionName
        {
            get { throw new NotImplementedException(); }
        }

        MainMenuEntry intro, play, submenu, exit;

        public MainMenuScreen()
        {
            prevEntry = "MenuUp";
            nextEntry = "MenuDown";
            selectedEntry = "MenuAccept";
            cancelMenu = "MenuCancel";

            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            Selected = Color.Yellow;
            Highlighted = Color.Red;
            Normal = Color.White;
        }

        public override void Initialize()
        {
            InputSystem input = ScreenSystem.InputSystem;

            input.NewAction(PreviousEntryActionName, Keys.Up);
            input.NewAction(NextEntryActionName, Keys.Down);
            input.NewAction(SelectedEntryActionName, Keys.Enter);
            input.NewAction(MenuCancelActionName, Keys.Escape);
        }
    }
}
