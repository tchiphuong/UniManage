using Serilog;
using Serilog.Events;
using Serilog.Context;
using System.Reflection;
using System.Text.Json;
using UniManage.Model.Common;

namespace UniManage.Core.Logging
{
    /// <summary>
    /// API logging manager với structured logging (Serilog implementation)
    /// </summary>
    public static class UniLogManager
    {
        // Serilog uses global Log.Logger

        /// <summary>
        /// Log API request/response với structured data
        /// </summary>
        public static void WriteApiLog(
            string apiName,
            string message,
            HeaderInfo? headerInfo = null,
            bool isException = false,
            Exception? exception = null)
        {
            // Serialize header info thành JSON nếu có
            string headerInfoJson = "";
            if (headerInfo != null)
            {
                try
                {
                    headerInfoJson = JsonSerializer.Serialize(headerInfo);
                }
                catch
                {
                    headerInfoJson = "Failed to serialize header info";
                }
            }

            // Log properties for context
            // Important: These properties are used by Serilog.Sinks.Map to split files
            // We use generic "general" if apiName is missing
            var safeApiName = string.IsNullOrWhiteSpace(apiName) ? "general" : apiName;
            
            // Push properties to context
            using (LogContext.PushProperty("ApiName", safeApiName))
            using (LogContext.PushProperty("Username", headerInfo?.Username ?? "Anonymous"))
            using (LogContext.PushProperty("Method", "API")) // Can be enhanced if passed
            using (LogContext.PushProperty("Path", "API"))
            using (LogContext.PushProperty("HeaderInfoJson", headerInfoJson))
            {
                var logMessage = $"API: {safeApiName}, HeaderInfo: {headerInfoJson}, Message: {message}";

                if (isException || exception != null)
                {
                    if (exception != null)
                        Log.Error(exception, logMessage);
                    else
                        Log.Error(logMessage);
                }
                else
                {
                    Log.Information(logMessage);
                }
            }
        }

        /// <summary>
        /// Log API request/response using CoreLogModel
        /// </summary>
        public static void WriteApiLog(CoreLogModel logData)
        {
            var apiName = logData.HeaderInfo?.ApiName ?? "Unknown";
            
            // Extract extra properties from CoreLogModel to push to Serilog Context
            using (LogContext.PushProperty("ApiName", apiName))
            using (LogContext.PushProperty("Username", logData.HeaderInfo?.Username ?? "Anonymous"))
            using (LogContext.PushProperty("Cid", logData.HeaderInfo?.CorrelationId ?? "N/A"))
            using (LogContext.PushProperty("Method", "API")) 
            using (LogContext.PushProperty("Path", "API"))
            using (LogContext.PushProperty("Status", logData.ReturnCode))
            using (LogContext.PushProperty("ExecutionTime", "0")) // CoreLogModel 'ms' property?
            {
                var message = logData.Message ?? (logData.IsException == 1 ? "Exception occurred" : "Success");
                
                if (logData.IsException == 1)
                {
                    Log.Error(message);
                }
                else
                {
                    Log.Information(message);
                }
            }
        }

        /// <summary>
        /// Initialize Serilog configuration
        /// </summary>
        /// <param name="logPath">Path to log directory</param>
        public static void Initialize(string logPath = "logs")
        {
            // Ensure absolute path
            if (!Path.IsPathRooted(logPath))
            {
                logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logPath);
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Map(le => le.Timestamp.ToString("yyyy-MM-dd"), (date, wt) =>
                {
                    // Inner Map: Split by API Name
                    wt.Map("ApiName", "general", (name, wt2) =>
                    {
                        var folder = Path.Combine(logPath, date);

                        // Main Log: logs/yyyy-MM-dd/{api}.log
                        wt2.File(Path.Combine(folder, $"{date}-{name}.log"),
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [user:{Username}] [ip:{IpAddress}] [cid:{Cid}] {Message:lj}{NewLine}{Exception}");

                        // Error Log: logs/yyyy-MM-dd/error-{api}.log
                        wt2.File(Path.Combine(folder, $"{date}-error-{name}.log"),
                            restrictedToMinimumLevel: LogEventLevel.Error,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [user:{Username}] [ip:{IpAddress}] {Message:lj}{NewLine}{Exception}");
                    });
                })
                .CreateLogger();
        }
    }
}
