using System.Diagnostics;

namespace GameEngine.Core
{
    public static class CoreAssert
    {
        public static void Assert(bool x, string message)
        {
            if (!x) 
            {
                Log.CoreLogger.Error("Assertion Failed:" + message);
                Debugger.Break();
            }
        }
    }
}
