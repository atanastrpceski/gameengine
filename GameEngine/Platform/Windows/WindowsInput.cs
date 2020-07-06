using GameEngine.Core.Window;
using OpenTK.Input;
using System;

namespace GameEngine.Platform.Windows
{
    public class WindowsInput : Input
    {
        public static void Init()
        {
            _instance = new WindowsInput();
        }

        protected override bool IsKeyPressedImpl(int keycode)
        {
            var state = Keyboard.GetState();
            return state[(short)keycode];
        }

        protected override Tuple<float, float> GetMousePositionImpl()
        {
            var cursorState = Mouse.GetCursorState();
            return new Tuple<float, float>(cursorState.X, cursorState.Y);
        }

        protected override bool IsMouseButtonPressedImpl(int button)
        {
            MouseButton mouseButton = MouseButton.Left;

            switch (button)
            {
                case 0:
                    mouseButton = MouseButton.Left;
                    break;
                case 1:
                    mouseButton = MouseButton.Right;
                    break;
                case 2:
                    mouseButton = MouseButton.Middle;
                    break;
                default:
                    break;
            }

            var state = Mouse.GetState();
            return state[mouseButton];
        }

        protected override float GetMouseXImpl()
        {
            return Mouse.GetCursorState().X;
        }
        protected override float GetMouseYImpl()
        {
            return Mouse.GetCursorState().Y;
        }
    }
}
