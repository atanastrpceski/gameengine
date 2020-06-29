using GameEngine.Core;
using GameEngine.Core.Events;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;

namespace GameEngine.Platform.Windows
{
    public class Window : IWindow
    {
        GameWindow _window;
        Core.EventHandler _eventHandler;

        public Window(WindowProp prop)
        {
            Init(prop);
        }

        void Init(WindowProp props)
        {
            Log.CoreLogger.Info("Creating window {0} ({1}, {2})", props.Title, props.Width, props.Height);

            var gameWindowSettings = GameWindowSettings.Default;
            var nativeWindowSettings = NativeWindowSettings.Default;

            nativeWindowSettings.Size = new OpenToolkit.Mathematics.Vector2i(props.Width, props.Height);
            nativeWindowSettings.Title = props.Title;

            _window = new GameWindow(gameWindowSettings, nativeWindowSettings);
            _window.MakeCurrent();

            SetVSync(true);

            //Set callbacks
            _window.Closed += HandleWindowClosed;
            _window.KeyUp += HandleKeyUp;
            _window.KeyDown += HandleKeyDown;
            _window.MouseMove += HandleMouseMoved;
            _window.MouseWheel += HandleMouseScroll;
            _window.Resize += HandleWindowSizeChanged;
        }

        private void HandleMouseMoved(MouseMoveEventArgs e)
        {
            _eventHandler?.Invoke(new MouseMovedEvent(e.X, e.Y));
        }

        private void HandleMouseScroll(MouseWheelEventArgs e)
        {
            _eventHandler?.Invoke(new MouseScrolledEvent(e.OffsetX, e.OffsetY));
        }

        private void HandleKeyUp(KeyboardKeyEventArgs e)
        {
            _eventHandler?.Invoke(new KeyReleasedEvent(e.Key));
        }

        private void HandleKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
            {
                _eventHandler?.Invoke(new KeyPressedEvent(e.Key, 1));
            }
            else
            {
                _eventHandler?.Invoke(new KeyPressedEvent(e.Key, 0));
            }
        }

        private void HandleWindowClosed()
        {
            _eventHandler?.Invoke(new WindowCloseEvent());
        }

        private void HandleWindowSizeChanged(ResizeEventArgs e)
        {
            _eventHandler?.Invoke(new WindowResizeEvent(_window.Size.X , _window.Size.Y));
        }

        public void Dispose()
        {
            _window.Closed -= HandleWindowClosed;
            _window.KeyUp -= HandleKeyUp;
            _window.KeyDown -= HandleKeyDown;
            _window.MouseMove -= HandleMouseMoved;
            _window.MouseWheel -= HandleMouseScroll;
            _window.Resize -= HandleWindowSizeChanged;
            _window.Close();
            _window.Dispose();
        }

        public void OnUpdate()
        {
            _window.ProcessEvents();
            _window.SwapBuffers();
        }

        public int GetWidth()
        {
            return _window.Size.X;
        }

        public int GetHeight()
        {
            return _window.Size.Y;
        }

        public void SetVSync(bool enabled)
        {
            if (enabled)
                _window.VSync = VSyncMode.On;
            else
                _window.VSync = VSyncMode.Off;
        }

        public bool IsVSync()
        {
            return _window.VSync == VSyncMode.On;
        }

        public void SetEventHandler(Core.EventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
    }
}
