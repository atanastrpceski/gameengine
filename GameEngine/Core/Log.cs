using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core
{
    public class Log
    {
        private static Logger _coreLogger;
        private static Logger _clientLogger;

        public static Logger CoreLogger { get => _coreLogger; }
        public static Logger ClientLogger { get => _clientLogger; }

        public static void Init()
        {
            _coreLogger = LogManager.GetLogger("Engine");
            _clientLogger = LogManager.GetLogger("App");
        }
    }
}
