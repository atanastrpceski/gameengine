using GameEngine.Core.Events;
using GameEngine.OpenGL;
using System;

namespace GameEngine.Core
{
    public class Application : IDisposable
    {
        IWindow _window;
        private bool _isRunning = true;
        private LayerStack _layerStack;

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

                foreach (var layer in _layerStack.Layers)
                {
                    layer.OnUpdate();
                }

                _window.OnUpdate();
            }
        }

        public void OnEvent(Event @event)
        {
            EventDispatcher<WindowCloseEvent>.Dispatch(@event, OnWindowClose);

            foreach (var layer in _layerStack.Layers)
            {
                layer.OnEvent(@event);
                if (@event.IsHandled)
                    break;
            }
        }

        private bool OnWindowClose()
        {
            Log.CoreLogger.Info("WindowCloseEvent");

            _isRunning = false;
            return true;
        }

        void PushLayer(Layer layer)
        {
            _layerStack.PushLayer(layer);
        }

        void PushOverlay(Layer layer)
        {
            _layerStack.PushOverlay(layer);
        }

        public void Dispose()
        {
            _window.Dispose();
        }
    }
}
