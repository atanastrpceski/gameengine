using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public interface IWindow : IDisposable
    {
        WindowData WindowData { get;}
        void OnUpdate();
        int GetWidth();
        int GetHeight();
        void SetVSync(bool enabled);
        bool IsVSync();
    }
}
