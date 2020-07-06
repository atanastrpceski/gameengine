using GameEngine.Core.Events;
using OpenTK;
using System;

namespace GameEngine.Core
{
    public delegate void EventHandler(Event @event);

    public interface IWindow : IDisposable
    {
        INativeWindow GetNativeWindow();

        void OnUpdate();
        int GetWidth();
        int GetHeight();
        void SetVSync(bool enabled);
        bool IsVSync();
        void SetEventHandler(EventHandler eventHandler);

    }
}
