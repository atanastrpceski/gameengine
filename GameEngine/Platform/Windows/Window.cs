using System;
using GLFW;
using System.Runtime.InteropServices;
using GameEngine.Core;
using GameEngine.Core.Events;

namespace GameEngine.Platform.Windows
{
    public class WindowData
    {
        public string Title;
        public int Width;
        public int Height;
        public bool VSync;
    };

    public class Window : IWindow
    {
        NativeWindow _window;

        WindowData _data;
        IntPtr _windowDataPtr;
        Core.EventHandler _eventHandler;

        static bool _isGLFWInitialized = false;

        public Window(WindowProp prop)
        {
            Init(prop);
        }

        void Init(WindowProp props)
        {
            _data = new WindowData
            {
                Title = props.Title,
                Width = props.Width,
                Height = props.Height
            };

            GCHandle handle = GCHandle.Alloc(_data);
            _windowDataPtr = (IntPtr)handle;

            Log.CoreLogger.Info("Creating window {0} ({1}, {2})", props.Title, props.Width, props.Height);

            if (!_isGLFWInitialized)
            {
                bool success = Glfw.Init();
                CoreAssert.Assert(success, "Could not intialize GLFW!");

                _isGLFWInitialized = true;
            }

            _window = new NativeWindow((int)props.Width, (int)props.Height, _data.Title, Monitor.None, GLFW.Window.None);
            Glfw.MakeContextCurrent(_window);
            Glfw.SetWindowUserPointer(_window, _windowDataPtr);
            SetVSync(true);

            //Set callbacks
            _window.SizeChanged += HandleWindowSizeChanged;
            _window.Closed += HandleWindowClosed;
        }

        private void HandleWindowClosed(object sender, EventArgs e)
        {
            WindowCloseEvent @event = new WindowCloseEvent();
            _eventHandler?.Invoke(@event);
        }

        private void HandleWindowSizeChanged(object sender, SizeChangeEventArgs e)
        {
            WindowResizeEvent @event = new WindowResizeEvent(e.Size.Width, e.Size.Height);
            _eventHandler?.Invoke(@event);
        }

        public void Dispose()
        {
            _window.SizeChanged -= HandleWindowSizeChanged;
            _window.Closed += HandleWindowClosed;

            if(!_window.IsClosed)
                Glfw.DestroyWindow(_window);
        }

        public void OnUpdate()
        {
            Glfw.PollEvents();
            Glfw.SwapBuffers(_window);
        }

        public int GetWidth()
        {
            throw new NotImplementedException();
        }

        public int GetHeight()
        {
            throw new NotImplementedException();
        }

        public void SetVSync(bool enabled)
        {
            if (enabled)
                Glfw.SwapInterval(1);
            else
                Glfw.SwapInterval(0);

            _data.VSync = enabled;
        }

        public bool IsVSync()
        {
            return _data.VSync;
        }

        public void SetEventHandler(Core.EventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
    }
}
