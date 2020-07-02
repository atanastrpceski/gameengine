using GameEngine.Core;
using GameEngine.Core.Events;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace GameEngine.Platform.Windows
{
    public class Window : IWindow
    {
        NativeWindow _window;
        EventHandler _eventHandler;

        private GraphicsContext _graphicsContext;

        public Window(WindowProp prop)
        {
            Init(prop);
        }

        void Init(WindowProp props)
        {
            Log.CoreLogger.Info("Creating window {0} ({1}, {2})", props.Title, props.Width, props.Height);

            _window = new NativeWindow(props.Width, props.Height, props.Title, GameWindowFlags.Default, GraphicsMode.Default, DisplayDevice.Default);

            GraphicsContextFlags flags = GraphicsContextFlags.Default;
            _graphicsContext = new GraphicsContext(GraphicsMode.Default, _window.WindowInfo, 3, 0, flags);
            _graphicsContext.MakeCurrent(_window.WindowInfo);
            ((IGraphicsContextInternal)_graphicsContext).LoadAll(); // wtf is this?

            SetVSync(true);

            _window.Visible = true;

            ////Set callbacks

            _window.Closed += HandleWindowClosed;
            _window.KeyUp += HandleKeyUp;
            _window.KeyDown += HandleKeyDown;
            _window.MouseMove += HandleMouseMoved;
            _window.MouseWheel += HandleMouseScroll;
            _window.Resize += HandleWindowSizeChanged;
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
            _eventHandler?.Invoke(new KeyReleasedEvent(e.Key, e.Modifiers));
        }

        private void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
            {
                _eventHandler?.Invoke(new KeyPressedEvent(e.Key, e.Modifiers, 1));
            }
            else
            {
                _eventHandler?.Invoke(new KeyPressedEvent(e.Key, e.Modifiers, 0));
            }
        }

        private void HandleWindowClosed(object sender, System.EventArgs e)
        {
            _eventHandler?.Invoke(new WindowCloseEvent());
        }

        private void HandleWindowSizeChanged(object sender, System.EventArgs e)
        {
            _eventHandler?.Invoke(new WindowResizeEvent(_window.Size.Width, _window.Size.Height));
        }

        public void Dispose()
        {
            _window.Closed -= HandleWindowClosed;
            _window.KeyUp -= HandleKeyUp;
            _window.KeyDown -= HandleKeyDown;
            _window.MouseMove -= HandleMouseMoved;
            _window.MouseWheel -= HandleMouseScroll;
            _window.Resize -= HandleWindowSizeChanged;

            _graphicsContext.Dispose();
            _window.Visible = false;

            _window.Close();
            _window.Dispose();
        }

        public void OnUpdate()
        {
            _window.ProcessEvents();

            try
            {
                if (_window.Visible || !_graphicsContext.IsDisposed)
                    _graphicsContext.SwapBuffers();
            }
            catch (System.Exception)
            {
            }
        }

        public int GetWidth()
        {
            return _window.Size.Width;
        }

        public int GetHeight()
        {
            return _window.Size.Height;
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

        public Rectangle GetBounds()
        {
            return _window.Bounds;
        }

        public Point PointToClient(Point point)
        {
            try
            {
                return _window.PointToClient(point);
            }
            catch (System.Exception)
            {
            }

            return Point.Empty;
        }
    }
}
