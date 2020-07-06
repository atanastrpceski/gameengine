using GameEngine.Core.Events;
using OpenTK;
using OpenTK.Graphics.ES20;
using System;

namespace GameEngine.Core
{
    public class Application : IDisposable
    {
        IWindow _window;
        private bool _isRunning = true;
        private LayerStack _layerStack;
        private static Application _instance = null;

        public Application(WindowProp prop)
        {
            CoreAssert.Assert(_instance == null, "Only one app can run at the time!");

            _layerStack = new LayerStack();

            Log.Init();

            _window = WindowFactory.Create(prop, PlatformEnum.Windows);
            _window.SetEventHandler(OnEvent);

            _instance = this;
        }

        public void Run()
        {
            while (_isRunning)
            {
                GL.ClearColor(Color.Black);
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

        private void OnWindowClose(WindowCloseEvent @event)
        {
            Log.CoreLogger.Info("WindowCloseEvent");
            _isRunning = false;
        }

        public void PushLayer(Layer layer)
        {
            _layerStack.PushLayer(layer);
            layer.OnAttach();
        }

        public void PushOverlay(Layer layer)
        {
            _layerStack.PushOverlay(layer);
            layer.OnAttach();
        }

        public void Dispose()
        {
            foreach (var layer in _layerStack.Layers)
            {
                layer.OnDetach();
            }

            _window.Dispose();
        }

        public static Application GetApplication()
        {
            return _instance;
        }

        public static IWindow GetWindow()
        {
            return _instance._window;
        }

        public static INativeWindow GetNativeWindow()
        {
            return _instance._window.GetNativeWindow();
        }
    }
}
