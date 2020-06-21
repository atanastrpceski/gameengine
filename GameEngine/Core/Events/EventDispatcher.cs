using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Events
{
    class EventDispatcher
    {
        private Event _event;

        public EventDispatcher(Event @event)
        {
            _event = @event;
        }
    }
}
