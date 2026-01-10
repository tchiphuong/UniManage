using Serilog;

namespace UniManage.Core.Logging
{
    /// <summary>
    /// Static logger utility - Wrapper for Serilog
    /// </summary>
    public static class UniLogger
    {
        // Serilog Logger is configured globally in Program.cs/UniLogManager.Initialize

        /// <summary>
        /// Log debug
        /// </summary>
        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        /// <summary>
        /// Log debug with exception
        /// </summary>
        public static void Debug(string message, Exception exception)
        {
            Log.Debug(exception, message);
        }

        /// <summary>
        /// Log info
        /// </summary>
        public static void Info(string message)
        {
            Log.Information(message);
        }

        /// <summary>
        /// Log info with exception
        /// </summary>
        public static void Info(string message, Exception exception)
        {
            Log.Information(exception, message);
        }

        /// <summary>
        /// Log warning
        /// </summary>
        public static void Warn(string message)
        {
            Log.Warning(message);
        }

        /// <summary>
        /// Log warning with exception
        /// </summary>
        public static void Warn(string message, Exception exception)
        {
            Log.Warning(exception, message);
        }

        /// <summary>
        /// Log error
        /// </summary>
        public static void Error(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Log error with exception
        /// </summary>
        public static void Error(string message, Exception exception)
        {
            Log.Error(exception, message);
        }

        /// <summary>
        /// Log fatal
        /// </summary>
        public static void Fatal(string message)
        {
            Log.Fatal(message);
        }

        /// <summary>
        /// Log fatal with exception
        /// </summary>
        public static void Fatal(string message, Exception exception)
        {
            Log.Fatal(exception, message);
        }

        /// <summary>
        /// Log info with format
        /// </summary>
        public static void InfoFormat(string format, params object[] args)
        {
            // Use string.Format to maintain compatibility with legacy log4net calls that expect index placeholders
            Log.Information(string.Format(format, args));
        }

        /// <summary>
        /// Log warning with format
        /// </summary>
        public static void WarnFormat(string format, params object[] args)
        {
            Log.Warning(string.Format(format, args));
        }

        /// <summary>
        /// Log error with format
        /// </summary>
        public static void ErrorFormat(string format, params object[] args)
        {
            Log.Error(string.Format(format, args));
        }

        // Checks (Serilog checks internally usually, but we map them)
        public static bool IsDebugEnabled => Log.IsEnabled(Serilog.Events.LogEventLevel.Debug);
        public static bool IsInfoEnabled => Log.IsEnabled(Serilog.Events.LogEventLevel.Information);
        public static bool IsWarnEnabled => Log.IsEnabled(Serilog.Events.LogEventLevel.Warning);
        public static bool IsErrorEnabled => Log.IsEnabled(Serilog.Events.LogEventLevel.Error);
        public static bool IsFatalEnabled => Log.IsEnabled(Serilog.Events.LogEventLevel.Fatal);
    }
}
