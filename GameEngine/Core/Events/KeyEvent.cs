
using GLFW;

namespace GameEngine.Core.Events
{
    public class KeyEvent : Event
    {
        protected Keys _keyCode;

        public Keys GetKeyCode()
        {
            return _keyCode;
        }

        protected KeyEvent(Keys key)
        {
            _category = EventCategory.Keyboard | EventCategory.Input;

            _keyCode = key;
        }
    }

    public class KeyPressedEvent  : KeyEvent
    {
        private readonly int _repeatCount;

        public int GetRepeatCount()
        {
            return _repeatCount;
        }
        public KeyPressedEvent(Keys key, int repeatCount) : base(key)
        {
            _eventType = EventType.KeyPressed;

            _repeatCount = repeatCount;
        }
        public override string ToString()
        {
            return $"KeyPressed: {_keyCode} ({_repeatCount} repeats)"; 
        }
    }

    public class KeyReleasedEvent : KeyEvent
    {

        public KeyReleasedEvent(Keys key) : base(key)
        {
            _eventType = EventType.KeyReleased;
        }
        public override string ToString()
        {
            return $"KeyReleased: {_keyCode}";
        }
    }

    public class KeyTypedEvent : KeyEvent
    {
        public KeyTypedEvent(Keys key, int repeatCount) : base(key)
        {
            _eventType = EventType.KeyTyped;
        }
        public override string ToString()
        {
            return $"KeyTyped: {_keyCode}";
        }
    }
}
