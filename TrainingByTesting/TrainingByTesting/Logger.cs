using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TrainingByTesting
{
    public static class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogDebug(string message) => Log.Debug(message);

        public static void LogError(string message) => Log.Error(message);

        public static void LogInfo(string message) => Log.Info(message);

        public static void LogWarn(string message) => Log.Warn(message);
    }
}
