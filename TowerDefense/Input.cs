﻿using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public static class Input
    {
        public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
        public static MouseState mouseState, oldMouseState = Mouse.GetState();

        public static bool IsKeyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }

        public static bool LeftClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed &&
                oldMouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightClick()
        {
            return mouseState.RightButton == ButtonState.Pressed &&
                oldMouseState.RightButton == ButtonState.Released;
        }

        //Should be called at beginning of Update in Game
        public static void Update()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

    }
}
