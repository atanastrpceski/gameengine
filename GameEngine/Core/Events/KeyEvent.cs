
using GLFW;

namespace GameEngine.Core.Events
{
    public class KeyEvent : Event
    {
        protected KeyEventArgs _keyCode;

        public KeyEventArgs GetKeyCode()
        {
            return _keyCode;
        }

        protected KeyEvent(KeyEventArgs key)
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
        public KeyPressedEvent(KeyEventArgs key, int repeatCount) : base(key)
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

        public KeyReleasedEvent(KeyEventArgs key) : base(key)
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
        public KeyTypedEvent(KeyEventArgs key, int repeatCount) : base(key)
        {
            _eventType = EventType.KeyTyped;
        }
        public override string ToString()
        {
            return $"KeyTyped: {_keyCode}";
        }
    }
}
