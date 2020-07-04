using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Events
{
    public class MouseScrolledEvent : Event
    {
        double _xOffset;
        double _yOffset;

        public double GetXOffset() { return _xOffset; }
        public double GetYOffset() { return _yOffset; }

        public MouseScrolledEvent(double xOffset, double yOffset)
        {
            _category = EventCategory.Mouse | EventCategory.Input;
            _eventType = EventType.MouseScrolled;

            _xOffset = xOffset;
            _yOffset = yOffset;
        }

        public override string ToString()
        {
            return $"MouseScrolledEvent: {GetXOffset()}, {GetYOffset()}";
        }
    }

    public class MouseMovedEvent : Event
    {
        double _mouseX;
        double _mouseY;

        public double GetXOffset() { return _mouseX; }
        public double GetYOffset() { return _mouseY; }

        public MouseMovedEvent(double mouseX, double mouseY)
        {
            _category = EventCategory.Mouse | EventCategory.Input;
            _eventType = EventType.MouseMoved;

            _mouseX = mouseX;
            _mouseY = mouseY;
        }

        public override string ToString()
        {
            return $"MouseMovedEvent: {_mouseX}, {_mouseY}";
        }
    }

    public class MouseButtonEvent : Event
    {
        protected int m_Button;

        public int GetMouseButton()
        {
            return m_Button;
        }

        public MouseButtonEvent(int button)
        {
            m_Button = button;
            _category = EventCategory.Mouse | EventCategory.Input;
        }
    }

    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        public MouseButtonPressedEvent(int button) : base(button)
        {
            _eventType = EventType.MouseButtonPressed;
        }

        public override string ToString()
        {
            return "MouseButtonPressedEvent: " + m_Button;
        }
    }

    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        public MouseButtonReleasedEvent(int button) : base(button)
        {
            _eventType = EventType.MouseButtonReleased;
        }
        public override string ToString()
        {
            return "MouseButtonReleasedEvent: " + m_Button;
        }
    }
}
