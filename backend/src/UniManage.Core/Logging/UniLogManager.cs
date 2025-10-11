using log4net;
using System.Reflection;
using System.Text.Json;
using UniManage.Core.Models;

namespace UniManage.Core.Logging
{
    /// <summary>
    /// API logging manager với structured logging
    /// </summary>
    public static class UniLogManager
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(UniLogManager));

        /// <summary>
        /// Log API request/response với structured data
        /// </summary>
        /// <param name="apiName">Tên API</param>
        /// <param name="message">Message</param>
        /// <param name="headerInfo">Header information</param>
        /// <param name="isException">Có phải exception không</param>
        /// <param name="exception">Exception nếu có</param>
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

            var logMessage = $"API: {apiName}, HeaderInfo: {headerInfoJson}, Message: {message}";

            if (isException && exception != null)
            {
                log.Error(logMessage, exception);
            }
            else if (isException)
            {
                log.Error(logMessage);
            }
            else
            {
                log.Info(logMessage);
            }
        }

        /// <summary>
        /// Log API performance
        /// </summary>
        /// <param name="apiName">API name</param>
        /// <param name="executionTimeMs">Execution time in milliseconds</param>
        /// <param name="statusCode">HTTP status code</param>
        public static void WritePerformanceLog(string apiName, long executionTimeMs, int statusCode)
        {
            var logMessage = $"API Performance - {apiName}: {executionTimeMs}ms, Status: {statusCode}";

            if (executionTimeMs > 5000) // Log as warning if > 5 seconds
            {
                log.Warn(logMessage);
            }
            else
            {
                log.Info(logMessage);
            }
        }

        /// <summary>
        /// Log database operation
        /// </summary>
        /// <param name="operation">Database operation name</param>
        /// <param name="tableName">Table name</param>
        /// <param name="executionTimeMs">Execution time</param>
        /// <param name="rowsAffected">Number of rows affected</param>
        public static void WriteDatabaseLog(string operation, string tableName, long executionTimeMs, int rowsAffected = 0)
        {
            var logMessage = $"DB Operation - {operation} on {tableName}: {executionTimeMs}ms, Rows: {rowsAffected}";
            log.Info(logMessage);
        }
    }
}
