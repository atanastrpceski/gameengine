using System;

namespace GameEngine.Core
{
    public static class WindowFactory
    {
        public static IWindow Create(WindowProp props, PlatformEnum platform)
        {
            switch (platform)
            {
                case PlatformEnum.Windows:
                    return new Platform.Windows.Window(props);
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            
        }
    }
}
