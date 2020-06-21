using GLFW;
using System.Runtime.InteropServices;

namespace GameEngine.OpenGL
{
    public static class Gl
    {
        public const int GL_COLOR_BUFFER_BIT = 0x00004000;

        public delegate void glClearColorHandler(float r, float g, float b, float a);
        public delegate void glClearHandler(int mask);

        public static glClearColorHandler glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(Glfw.GetProcAddress("glClearColor"));
        public static glClearHandler glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(Glfw.GetProcAddress("glClear"));
    }
}
