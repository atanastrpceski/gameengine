using GameEngine;

namespace GameEngine.Core
{
    public class EntryPoint
    {
        private readonly Application _app;
        private readonly string[] _args;

        public EntryPoint(Application app, string[] args)
        {
            _app = app;
            _args = args;

            Log.Init();

            Log.CoreLogger.Warn("Init");
            Log.ClientLogger.Warn("Init");

            _app.Run();
            _app.Dispose();
        }
    }
}
