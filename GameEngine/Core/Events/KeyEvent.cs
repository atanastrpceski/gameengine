using OpenTK.Input;

namespace GameEngine.Core.Events
{
    public class KeyEvent : Event
    {
        protected Key _keyCode;
        protected KeyModifiers _modifiers;

        public Key GetKeyCode()
        {
            return _keyCode;
        }

        public KeyModifiers GetKeyModifiers()
        {
            return _modifiers;
        }

        protected KeyEvent(Key key, KeyModifiers modifiers)
        {
            _category = EventCategory.Keyboard | EventCategory.Input;
            _modifiers = modifiers;
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
        public KeyPressedEvent(Key key, KeyModifiers keyModifiers, int repeatCount) : base(key, keyModifiers)
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

        public KeyReleasedEvent(Key key, KeyModifiers keyModifiers) : base(key, keyModifiers)
        {
            _eventType = EventType.KeyReleased;
        }

        public override string ToString()
        {
            return $"KeyReleased: {_keyCode}";
        }
    }

    public class KeyTypedEvent : Event
    {
        protected char _keyCode;
        protected KeyModifiers _modifiers;

        public char GetChar()
        {
            return _keyCode;
        }

        public KeyTypedEvent(char keyCode)
        {
            _keyCode = keyCode;

            _eventType = EventType.KeyTyped;
            _category = EventCategory.Keyboard | EventCategory.Input;
        }
        public override string ToString()
        {
            return $"KeyTypedEvent: {_keyCode}";
        }
    }
}
