using System;

namespace GameEngine.Core.Events
{
    internal static class EventDispatcher<T>
        where T : Event
    {
        internal delegate bool TriggerEvent();

        internal static void Dispatch(Event @event, TriggerEvent action)
        {
            if (@event.IsHandled)
                return;

            if (@event.GetType() == typeof(T))
            {
                @event.IsHandled = true;
                action?.Invoke();
                @event = null;
            }
        }
    }
}
