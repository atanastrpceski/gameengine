using System;

namespace GameEngine.Core.Window
{
    public class Input
    {
        protected static Input _instance;

        public static bool IsKeyPressed(int keycode) 
        {
            return _instance.IsKeyPressedImpl(keycode);
        }


        protected virtual bool IsKeyPressedImpl(int keycode)
        {
            return false;
        }

        public static bool IsMouseButtonPressed(int button)
        {
            return _instance.IsKeyPressedImpl(button);
        }

        protected virtual bool IsMouseButtonPressedImpl(int button)
        {
            return false;
        }

        public static Tuple<float, float> GetMousePosition()
        {
            return _instance.GetMousePositionImpl();
        }
        protected virtual Tuple<float, float> GetMousePositionImpl()
        {
            return new Tuple<float, float>(0, 0);
        }
        public static float GetMouseX()
        {
            return _instance.GetMouseXImpl();
        }
        protected virtual float GetMouseXImpl()
        {
            return 0;
        }
        public static float GetMouseY()
        {
            return _instance.GetMouseYImpl();
        }

        protected virtual float GetMouseYImpl()
        {
            return 0;
        }
    }
}
