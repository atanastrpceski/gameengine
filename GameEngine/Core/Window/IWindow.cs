using GameEngine.Core.Events;
using System;

namespace GameEngine.Core
{
    public delegate void EventHandler(Event @event);

    public interface IWindow : IDisposable
    {
        void OnUpdate();
        int GetWidth();
        int GetHeight();
        void SetVSync(bool enabled);
        bool IsVSync();
        void SetEventHandler(EventHandler eventHandler);

    }
}
