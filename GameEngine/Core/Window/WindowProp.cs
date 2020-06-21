using System.Runtime.InteropServices;

namespace GameEngine.Core
{
    public class WindowProp
    {
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowData
    {
        public string Title;
        public int Width;
        public int Height;
        public bool VSync;
    };
}
