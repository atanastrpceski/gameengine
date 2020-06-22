using System;
using GLFW;
using System.Runtime.InteropServices;
using GameEngine.Core;

namespace GameEngine.Platform.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowData
    {
        public string Title;
        public int Width;
        public int Height;
        public bool VSync;
    };

    public class Window : IWindow
    {
        GLFW.Window _window;

        WindowData _windowData;
        IntPtr _windowDataPtr;

        static bool _isGLFWInitialized = false;

        public Window(WindowProp prop)
        {
            Init(prop);
        }

        void Init(WindowProp props)
        {
            _windowData.Title = props.Title;
            _windowData.Width = props.Width;
            _windowData.Height = props.Height;

            _windowDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_windowData));
            Marshal.StructureToPtr(_windowData, _windowDataPtr, false);

            Log.CoreLogger.Info("Creating window {0} ({1}, {2})", props.Title, props.Width, props.Height);

            if (!_isGLFWInitialized)
            {
                bool success = Glfw.Init();
                CoreAssert.Assert(success, "Could not intialize GLFW!");

                _isGLFWInitialized = true;
            }

            _window = Glfw.CreateWindow((int)props.Width, (int)props.Height, _windowData.Title, Monitor.None, GLFW.Window.None);
            Glfw.MakeContextCurrent(_window);
            Glfw.SetWindowUserPointer(_window, _windowDataPtr);
            SetVSync(true);
        }

        public void Dispose()
        {
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

            _windowData.VSync = enabled;
        }

        public bool IsVSync()
        {
            return _windowData.VSync;
        }
    }
}
