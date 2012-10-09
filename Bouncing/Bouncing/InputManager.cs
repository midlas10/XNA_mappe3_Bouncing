using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bouncing 
{
    class InputManager
    {
#if WINDOWS_PHONE
        public const int MaxInputs = 1;
#else
        public const int MaxInputs = 4;
#endif
        KeyboardState _curKeyboardState;
        KeyboardState _prevKeyboardState;
        MouseState _curMouseState;
        MouseState _prevMouseState;

        /// <summary>
        /// if GamePad is availbile
        /// </summary>
        GamePadState[] _curGamePadState;
        GamePadState[] _prevGamePadState;

        public enum MouseButtons
        {
            LeftButton,
            RightButton,
            MiddleButton
        }

        public InputManager()
        {
            _curKeyboardState = Keyboard.GetState();
            _prevKeyboardState = _curKeyboardState;
            _curMouseState = Mouse.GetState();
            _prevMouseState = _curMouseState;
            
            _curGamePadState = new GamePadState[MaxInputs];
            _prevGamePadState = new GamePadState[MaxInputs];
        }

        public void Update()
        {
            _prevKeyboardState = _curKeyboardState;
            _curKeyboardState = Keyboard.GetState();
            _prevMouseState = _curMouseState;
            _curMouseState = Mouse.GetState();

            _prevGamePadState[0] = _curGamePadState[0];
            _prevGamePadState[1] = _curGamePadState[1];
            _prevGamePadState[2] = _curGamePadState[2];
            _prevGamePadState[3] = _curGamePadState[3];

            _curGamePadState[0] = GamePad.GetState(PlayerIndex.One);
            _curGamePadState[1] = GamePad.GetState(PlayerIndex.Two);
            _curGamePadState[2] = GamePad.GetState(PlayerIndex.Three);
            _curGamePadState[3] = GamePad.GetState(PlayerIndex.Four);
        }


        /// <summary>
        /// returns true if key is pressed this frame
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>bool</returns>
        public bool IsKeyPressed(Keys key)
        {
            return (_curKeyboardState.IsKeyDown(key) && _prevKeyboardState.IsKeyUp(key));
        }

        /// <summary>
        /// returns true if key is released this frame
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>bool</returns>
        public bool IsKeyReleased(Keys key)
        {
            return _prevKeyboardState.IsKeyDown(key) && _curKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// returns true if key is down
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>bool</returns>
        public bool IsKeyDown(Keys key)
        {
            return _curKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Returns true if key is up
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>bool</returns>
        public bool isKeyUp(Keys key)
        {
            return _curKeyboardState.IsKeyUp(key);
        }

        public Keys[] GetDownKeys()
        {
            return _curKeyboardState.GetPressedKeys();
        }

        /// <summary>
        /// Returns true if any key is pressed on the keyboord
        /// </summary>
        /// <returns>bool</returns>
        public bool IsAnyKeyPressed()
        {
            return _curKeyboardState.GetPressedKeys().Length == 0;
        }

        /// <summary>
        /// Returns a vector where the mouse position is right now
        /// </summary>
        /// <returns>Vector2</returns>
        public Vector2 GetCurrentMousePosition()
        {
            return new Vector2(_curMouseState.X, _curMouseState.Y);
        }

        /// <summary>
        /// Return true if a mousebutton sendt is pressed
        /// </summary>
        /// <param name="button">MouseButtons</param>
        /// <returns>bool</returns>
        public bool IsMouseButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    {
                        if (_curMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released) return true;
                    }
                    break;
                case MouseButtons.RightButton:
                    {

                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Return true if the button asked for is pressed of the playerindex this frame
        /// </summary>
        /// <param name="button">Buttons</param>
        /// <param name="index">PlayerIndex</param>
        /// <returns>bool</returns>
        public bool IsGamePadButtonPressed(Buttons button, PlayerIndex index)
        {
            int curPlayer = (int)index;
            if (_curGamePadState[curPlayer].IsConnected)
            {
                return _curGamePadState[curPlayer].IsButtonDown(button) && _prevGamePadState[curPlayer].IsButtonUp(button);
            }
            return false;
        }

        /// <summary>
        /// Return true if the player from playerindex released the button checked for this frame
        /// </summary>
        /// <param name="button">Buttons</param>
        /// <param name="index">PlayerIndex</param>
        /// <returns></returns>
        public bool IsGamePadButtonReleased(Buttons button, PlayerIndex index)
        {
            int curPlayer = (int)index;
            if (_curGamePadState[curPlayer].IsConnected)
            {
                return _curGamePadState[curPlayer].IsButtonUp(button) && _prevGamePadState[curPlayer].IsButtonDown(button);
            }
            return false;
        }

        public bool IsGamePadButtonDown(Buttons button, PlayerIndex index)
        {
            int curPlayer = (int)index;
            
            if (_curGamePadState[curPlayer].IsConnected)
            {
                return _curGamePadState[curPlayer].IsButtonDown(button) && _prevGamePadState[curPlayer].IsButtonDown(button);
            }
            return false;
        }

        public bool IsGamePadButtonUp(Buttons button, PlayerIndex index)
        {
            if (index == PlayerIndex.One)
            {
                return _curGamePadState[0].IsButtonUp(button) && _prevGamePadState[0].IsButtonUp(button);
            }
            else if (index == PlayerIndex.Two)
            {
                return _curGamePadState[1].IsButtonUp(button) && _prevGamePadState[1].IsButtonUp(button);
            }
            else if (index == PlayerIndex.Three)
            {
                return _curGamePadState[2].IsButtonUp(button) && _prevGamePadState[2].IsButtonUp(button);
            }
            else if (index == PlayerIndex.Four)
            {
                return _curGamePadState[3].IsButtonUp(button) && _prevGamePadState[3].IsButtonUp(button);
            }
            return false;
        }

        public bool IsGamePadButtonPressed(Buttons button)
        {
            if (_curGamePadState[0].IsButtonDown(button) && _prevGamePadState[0].IsButtonUp(button)) {
                return true;
            }
            else if (_curGamePadState[1].IsButtonDown(button) && _prevGamePadState[1].IsButtonUp(button))
            {
                return true;
            }
            else if (_curGamePadState[2].IsButtonDown(button) && _prevGamePadState[2].IsButtonUp(button))
            {
                return true;
            }
            else if (_curGamePadState[3].IsButtonDown(button) && _prevGamePadState[3].IsButtonUp(button))
            {
                return true;
            }
            return false;
        }

        public bool IsGamePadButtonReleased(Buttons button)
        {
            if (_curGamePadState[0].IsButtonUp(button) && _prevGamePadState[0].IsButtonDown(button))
            {
                return true;
            }
            else if (_curGamePadState[1].IsButtonUp(button) && _prevGamePadState[1].IsButtonDown(button))
            {
                return true;
            }
            else if (_curGamePadState[2].IsButtonUp(button) && _prevGamePadState[2].IsButtonDown(button))
            {
                return true;
            }
            else if (_curGamePadState[3].IsButtonUp(button) && _prevGamePadState[3].IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        public bool IsGamePadButtonDown(Buttons button)
        {
            if (_curGamePadState[0].IsButtonUp(button) && _prevGamePadState[0].IsButtonDown(button))
            {
                return true;
            }
            else if (_curGamePadState[1].IsButtonUp(button) && _prevGamePadState[1].IsButtonDown(button))
            {
                return true;
            }
            else if (_curGamePadState[2].IsButtonUp(button) && _prevGamePadState[2].IsButtonDown(button))
            {
                return true;
            }
            else if (_curGamePadState[3].IsButtonUp(button) && _prevGamePadState[3].IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        public bool IsGamePadButtonUp(Buttons button)
        {
            if (_curGamePadState[0].IsButtonUp(button) && _prevGamePadState[0].IsButtonUp(button))
            {
                return true;
            }
            else if (_curGamePadState[1].IsButtonUp(button) && _prevGamePadState[1].IsButtonUp(button))
            {
                return true;
            }
            else if (_curGamePadState[2].IsButtonUp(button) && _prevGamePadState[2].IsButtonUp(button))
            {
                return true;
            }
            else if (_curGamePadState[3].IsButtonUp(button) && _prevGamePadState[3].IsButtonUp(button))
            {
                return true;
            }
            return false;
        }
    }
}