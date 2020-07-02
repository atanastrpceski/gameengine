using GameEngine.Core.Events;
using OpenTK;
using System;

namespace GameEngine.Core
{
    public delegate void EventHandler(Event @event);

    public interface IWindow : IDisposable
    {
        Point PointToClient(Point point);
        public Rectangle GetBounds();
        void OnUpdate();
        int GetWidth();
        int GetHeight();
        void SetVSync(bool enabled);
        bool IsVSync();
        void SetEventHandler(EventHandler eventHandler);

    }
}
