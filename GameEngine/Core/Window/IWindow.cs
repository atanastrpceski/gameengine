using System;

namespace GameEngine.Core
{
    public interface IWindow : IDisposable
    {
        void OnUpdate();
        int GetWidth();
        int GetHeight();
        void SetVSync(bool enabled);
        bool IsVSync();
    }
}
