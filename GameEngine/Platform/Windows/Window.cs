using GLFW;
using System;
using System.Runtime.InteropServices;
using GameEngine.Core;
using GameEngine.Core.Events;

namespace GameEngine.Platform.Windows
{
    public class WindowData
    {
        public bool VSync;
        public int Width;
        public int Height;
        public string Title;
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

                Glfw.SetErrorCallback(HandleError);
                _isGLFWInitialized = true;
            }

            _window = new NativeWindow((int)props.Width, (int)props.Height, _data.Title, Monitor.None, GLFW.Window.None);
            Glfw.MakeContextCurrent(_window);
            Glfw.SetWindowUserPointer(_window, _windowDataPtr);
            SetVSync(true);

            //Set callbacks
            _window.Closed += HandleWindowClosed;
            _window.KeyAction += HandleKeyAction;
            _window.MouseMoved += HandleMouseMoved;
            _window.MouseScroll += HandleMouseScroll;
            _window.SizeChanged += HandleWindowSizeChanged;
        }

        private void HandleMouseMoved(object sender, MouseMoveEventArgs e)
        {
            _eventHandler?.Invoke(new MouseMovedEvent(e.X, e.Y));
        }

        private void HandleMouseScroll(object sender, MouseMoveEventArgs e)
        {
            _eventHandler?.Invoke(new MouseScrolledEvent(e.X, e.Y));
        }

        private void HandleKeyAction(object sender, KeyEventArgs e)
        {
            switch (e.State)
            {
                case InputState.Press:
                    {
                        _eventHandler?.Invoke(new KeyPressedEvent(e.Key, 0));
                        break;
                    }
                case InputState.Release:
                    {
                        _eventHandler?.Invoke(new KeyReleasedEvent(e.Key));
                        break;
                    }
                case InputState.Repeat:
                    {
                        _eventHandler?.Invoke(new KeyPressedEvent(e.Key, 1));
                        break;
                    }
            }
        }

        private void HandleError(ErrorCode code, IntPtr message)
        {
            if (code != ErrorCode.None)
            {
                GCHandle handle = (GCHandle)message;
                string description = handle.Target as string;

                Log.CoreLogger.Error($"GLFW Error ({code}): {description}");
            }
        }

        private void HandleWindowClosed(object sender, EventArgs e)
        {
            _eventHandler?.Invoke(new WindowCloseEvent());
        }

        private void HandleWindowSizeChanged(object sender, SizeChangeEventArgs e)
        {
            _data.Width = e.Size.Width;
            _data.Height = e.Size.Height;
            _eventHandler?.Invoke(new WindowResizeEvent(e.Size.Width, e.Size.Height));
        }

        public void Dispose()
        {
            _window.Closed += HandleWindowClosed;
            _window.KeyAction -= HandleKeyAction;
            _window.MouseMoved -= HandleMouseMoved;
            _window.SizeChanged -= HandleWindowSizeChanged;
            _window.MouseScroll -= HandleMouseScroll;

            if (!_window.IsClosed)
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
