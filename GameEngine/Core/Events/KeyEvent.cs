
namespace GameEngine.Core.Events
{
    public class KeyEvent : Event
    {
        protected Key _keyCode;

        public Key GetKeyCode()
        {
            return _keyCode;
        }

        protected KeyEvent(Key key)
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
        public KeyPressedEvent(Key key, int repeatCount) : base(key)
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

        public KeyReleasedEvent(Key key) : base(key)
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
        public KeyTypedEvent(Key key, int repeatCount) : base(key)
        {
            _eventType = EventType.KeyTyped;
        }
        public override string ToString()
        {
            return $"KeyTyped: {_keyCode}";
        }
    }
}
