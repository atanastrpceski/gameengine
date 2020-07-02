using System;

namespace GameEngine.Core.Events
{
    internal static class EventDispatcher<T>
        where T : Event
    {
        internal delegate void TriggerEvent(T @event);

        internal static void Dispatch(Event @event, Action<T> action)
        {
            if (@event.IsHandled)
                return;

            if (@event.GetType() == typeof(T))
            {
                @event.IsHandled = true;
                action?.Invoke((T)@event);
                @event = null;
            }
        }
    }
}
