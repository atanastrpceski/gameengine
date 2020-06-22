using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Events
{
    public class WindowResizeEvent : Event
    {
        private readonly int _width;
        private readonly int _height;

        public WindowResizeEvent(int width, int height)
        {
            _eventType = EventType.WindowResize;
            _category = EventCategory.Application;

            this._width = width;
            this._height = height;
        }

        public override string ToString()
        {
            return $"WindowResizeEvent: {_width}, {_height}";
        }
    }


    public class WindowCloseEvent : Event 
    {
        public WindowCloseEvent()
        {
            _eventType = EventType.WindowClose;
            _category = EventCategory.Application;
        }
    }
}
