using GameEngine.Core.Events;
using GameEngine.OpenGL;
using System;

namespace GameEngine.Core
{
    public class Application : IDisposable
    {
        IWindow _window;
        private bool _isRunning = true;

        public Application(WindowProp prop)
        {
            Log.Init();

            _window = WindowFactory.Create(prop, PlatformEnum.Windows);
            _window.SetEventHandler(OnEvent);
        }

        public void Run()
        {
            while (_isRunning)
            {
                Gl.glClearColor(1, 0, 1, 1);
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
                _window.OnUpdate();
            }
        }

        public void OnEvent(Event @event)
        {
            EventDispatcher<WindowCloseEvent>.Dispatch(@event, OnWindowClose);
        }

        private bool OnWindowClose()
        {
            _isRunning = false;
            return true;
        }


        public void Dispose()
        {
            _window.Dispose();
        }
    }
}
