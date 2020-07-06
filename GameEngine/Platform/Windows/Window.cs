using GameEngine.Core;
using GameEngine.Core.Events;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace GameEngine.Platform.Windows
{
    public class Window : IWindow
    {
        NativeWindow _nativeWindow;
        EventHandler _eventHandler;

        private GraphicsContext _graphicsContext;

        public Window(WindowProp prop)
        {
            Init(prop);
        }

        void Init(WindowProp props)
        {
            Log.CoreLogger.Info("Creating window {0} ({1}, {2})", props.Title, props.Width, props.Height);

            _nativeWindow = new NativeWindow(props.Width, props.Height, props.Title, GameWindowFlags.Default, GraphicsMode.Default, DisplayDevice.Default);

            GraphicsContextFlags flags = GraphicsContextFlags.Default;
            _graphicsContext = new GraphicsContext(GraphicsMode.Default, _nativeWindow.WindowInfo, 3, 0, flags);
            _graphicsContext.MakeCurrent(_nativeWindow.WindowInfo);
            ((IGraphicsContextInternal)_graphicsContext).LoadAll(); // wtf is this?

            SetVSync(true);

            _nativeWindow.Visible = true;

            ////Set callbacks

            _nativeWindow.Closed += HandleWindowClosed;
            _nativeWindow.KeyPress += HandleWindowKeyPress;
            _nativeWindow.KeyUp += HandleKeyUp;
            _nativeWindow.KeyDown += HandleKeyDown;
            _nativeWindow.MouseMove += HandleMouseMoved;
            _nativeWindow.MouseWheel += HandleMouseScroll;
            _nativeWindow.Resize += HandleWindowSizeChanged;
            _nativeWindow.MouseUp += HandleMouseUp;
            _nativeWindow.MouseDown += HandleMouseDown;
        }

        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = 0;
            if (e.Mouse.LeftButton == ButtonState.Pressed)
            {
                button = 0;
            }
            if (e.Mouse.RightButton == ButtonState.Pressed)
            {
                button = 1;
            }
            if (e.Mouse.MiddleButton == ButtonState.Pressed)
            {
                button = 2;
            }
            _eventHandler?.Invoke(new MouseButtonPressedEvent(button));
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            var button = 0;
            if (e.Mouse.LeftButton == ButtonState.Released)
            {
                button = 0;
            }
            if (e.Mouse.RightButton == ButtonState.Released)
            {
                button = 1;
            }
            if (e.Mouse.MiddleButton == ButtonState.Released)
            {
                button = 2;
            }
            _eventHandler?.Invoke(new MouseButtonReleasedEvent(button));
        }

        private void HandleWindowKeyPress(object sender, KeyPressEventArgs e)
        {
            _eventHandler?.Invoke(new KeyTypedEvent(e.KeyChar));
        }

        private void HandleMouseMoved(object sender, MouseMoveEventArgs e)
        {
            _eventHandler?.Invoke(new MouseMovedEvent(e.X, e.Y));
        }

        private void HandleMouseScroll(object sender, MouseWheelEventArgs e)
        {
            _eventHandler?.Invoke(new MouseScrolledEvent(e.X, e.Y));
        }

        private void HandleKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            _eventHandler?.Invoke(new KeyReleasedEvent((int)e.Key, e.Modifiers));
        }

        private void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
            {
                _eventHandler?.Invoke(new KeyPressedEvent((int)e.Key, e.Modifiers, 1));
            }
            else
            {
                _eventHandler?.Invoke(new KeyPressedEvent((int)e.Key, e.Modifiers, 0));
            }
        }

        private void HandleWindowClosed(object sender, System.EventArgs e)
        {
            _eventHandler?.Invoke(new WindowCloseEvent());
        }

        private void HandleWindowSizeChanged(object sender, System.EventArgs e)
        {
            _eventHandler?.Invoke(new WindowResizeEvent(_nativeWindow.Size.Width, _nativeWindow.Size.Height));
        }

        public void Dispose()
        {
            _nativeWindow.Closed -= HandleWindowClosed;
            _nativeWindow.KeyPress -= HandleWindowKeyPress;
            _nativeWindow.KeyUp -= HandleKeyUp;
            _nativeWindow.KeyDown -= HandleKeyDown;
            _nativeWindow.MouseMove -= HandleMouseMoved;
            _nativeWindow.MouseWheel -= HandleMouseScroll;
            _nativeWindow.Resize -= HandleWindowSizeChanged;
            _nativeWindow.MouseUp -= HandleMouseUp;
            _nativeWindow.MouseDown -= HandleMouseDown;

            _graphicsContext.Dispose();
            _nativeWindow.Visible = false;

            _nativeWindow.Close();
            _nativeWindow.Dispose();
        }

        public INativeWindow GetNativeWindow()
        {
            return _nativeWindow;
        }

        public void OnUpdate()
        {
            _nativeWindow.ProcessEvents();

            try
            {
                if (_nativeWindow.Visible || !_graphicsContext.IsDisposed)
                    _graphicsContext.SwapBuffers();
            }
            catch (System.Exception)
            {
            }
        }

        public int GetWidth()
        {
            return _nativeWindow.Size.Width;
        }

        public int GetHeight()
        {
            return _nativeWindow.Size.Height;
        }

        public void SetVSync(bool enabled)
        {
            if (enabled)
                _graphicsContext.VSync = true;
            else
                _graphicsContext.VSync = false;
        }

        public bool IsVSync()
        {
            return _graphicsContext.VSync;
        }

        public void SetEventHandler(EventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
    }
}
