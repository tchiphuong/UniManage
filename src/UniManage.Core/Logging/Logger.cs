using System;
using log4net;
using log4net.Core;

namespace UniManage.Core.Logging
{
    public static class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Logger));

        public static void LogInformation(string message, params object[] args)
        {
            Log.Info(string.Format(message, args));
        }

        public static void LogError(Exception ex, string message, params object[] args)
        {
            Log.Error(string.Format(message, args), ex);
        }

        public static void LogWarning(string message, params object[] args)
        {
            Log.Warn(string.Format(message, args));
        }

        public static void LogDebug(string message, params object[] args)
        {
            Log.Debug(string.Format(message, args));
        }
    }
}
