using GameEngine.Core.Events;
using OpenToolkit.Graphics.ES30;
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
            _layerStack = new LayerStack();
            Log.Init();

            _window = WindowFactory.Create(prop, PlatformEnum.Windows);
            _window.SetEventHandler(OnEvent);
        }

        public void Run()
        {
            while (_isRunning)
            {
                GL.ClearColor(1, 0, 1, 1);
                GL.Clear(ClearBufferMask.ColorBufferBit);

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
