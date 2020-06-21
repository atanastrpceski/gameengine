using System;

namespace GameEngine.Core.Events
{
    public enum EventType
    {
        None = 0,

        WindowClose, 
        WindowResize, 
        WindowFocus, 
        WindowLostFocus, 
        WindowMoved,

        AppTick, 
        AppUpdate, 
        AppRender,

        KeyPressed, 
        KeyReleased,
        KeyTyped,

        MouseButtonPressed, 
        MouseButtonReleased, 
        MouseMoved, 
        MouseScrolled
    }

    [Flags]
    public enum EventCategory
    {
        None,
        Application,
        Input,
        Keyboard,
        Mouse,
        MouseButton
    }

    public class Event
    {
        protected EventCategory _category;
        protected EventType _eventType;

        public bool IsHandled { get; set; }

        public virtual EventCategory GetCategoryFlags() {
            return _category;
        }

        public bool IsInCategory(EventCategory category)
        {
            return category.HasFlag(GetCategoryFlags());
        }

        public EventType GetEventType() {
            return _eventType;
        }
    }
}
