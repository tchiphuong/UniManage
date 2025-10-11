using log4net;
using log4net.Config;
using System.Reflection;

namespace UniManage.Core.Logging
{
    /// <summary>
    /// Static logger utility - không cần dependency injection
    /// Sử dụng log4net, tự động load config từ app.config
    /// </summary>
    public static class UniLogger
    {
        private static readonly ILog _logger;
        private static bool _isConfigured = false;

        static UniLogger()
        {
            // Auto-configure log4net từ app.config
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("app.config"));

            _logger = LogManager.GetLogger(typeof(UniLogger));
            _isConfigured = true;
        }

        /// <summary>
        /// Log thông tin debug (chỉ hiển thị khi Debug mode)
        /// </summary>
        public static void Debug(string message)
        {
            if (_isConfigured && _logger.IsDebugEnabled)
            {
                _logger.Debug(message);
            }
        }

        /// <summary>
        /// Log thông tin debug với exception
        /// </summary>
        public static void Debug(string message, Exception exception)
        {
            if (_isConfigured && _logger.IsDebugEnabled)
            {
                _logger.Debug(message, exception);
            }
        }

        /// <summary>
        /// Log thông tin (Info level)
        /// </summary>
        public static void Info(string message)
        {
            if (_isConfigured && _logger.IsInfoEnabled)
            {
                _logger.Info(message);
            }
        }

        /// <summary>
        /// Log thông tin với exception
        /// </summary>
        public static void Info(string message, Exception exception)
        {
            if (_isConfigured && _logger.IsInfoEnabled)
            {
                _logger.Info(message, exception);
            }
        }

        /// <summary>
        /// Log cảnh báo (Warning)
        /// </summary>
        public static void Warn(string message)
        {
            if (_isConfigured && _logger.IsWarnEnabled)
            {
                _logger.Warn(message);
            }
        }

        /// <summary>
        /// Log cảnh báo với exception
        /// </summary>
        public static void Warn(string message, Exception exception)
        {
            if (_isConfigured && _logger.IsWarnEnabled)
            {
                _logger.Warn(message, exception);
            }
        }

        /// <summary>
        /// Log lỗi (Error)
        /// </summary>
        public static void Error(string message)
        {
            if (_isConfigured && _logger.IsErrorEnabled)
            {
                _logger.Error(message);
            }
        }

        /// <summary>
        /// Log lỗi với exception
        /// </summary>
        public static void Error(string message, Exception exception)
        {
            if (_isConfigured && _logger.IsErrorEnabled)
            {
                _logger.Error(message, exception);
            }
        }

        /// <summary>
        /// Log lỗi nghiêm trọng (Fatal)
        /// </summary>
        public static void Fatal(string message)
        {
            if (_isConfigured && _logger.IsFatalEnabled)
            {
                _logger.Fatal(message);
            }
        }

        /// <summary>
        /// Log lỗi nghiêm trọng với exception
        /// </summary>
        public static void Fatal(string message, Exception exception)
        {
            if (_isConfigured && _logger.IsFatalEnabled)
            {
                _logger.Fatal(message, exception);
            }
        }

        /// <summary>
        /// Log với format string (tránh string interpolation nếu log level disabled)
        /// </summary>
        public static void InfoFormat(string format, params object[] args)
        {
            if (_isConfigured && _logger.IsInfoEnabled)
            {
                _logger.InfoFormat(format, args);
            }
        }

        /// <summary>
        /// Log warning với format string
        /// </summary>
        public static void WarnFormat(string format, params object[] args)
        {
            if (_isConfigured && _logger.IsWarnEnabled)
            {
                _logger.WarnFormat(format, args);
            }
        }

        /// <summary>
        /// Log error với format string
        /// </summary>
        public static void ErrorFormat(string format, params object[] args)
        {
            if (_isConfigured && _logger.IsErrorEnabled)
            {
                _logger.ErrorFormat(format, args);
            }
        }

        /// <summary>
        /// Kiểm tra log level có enabled không
        /// </summary>
        public static bool IsDebugEnabled => _isConfigured && _logger.IsDebugEnabled;
        public static bool IsInfoEnabled => _isConfigured && _logger.IsInfoEnabled;
        public static bool IsWarnEnabled => _isConfigured && _logger.IsWarnEnabled;
        public static bool IsErrorEnabled => _isConfigured && _logger.IsErrorEnabled;
        public static bool IsFatalEnabled => _isConfigured && _logger.IsFatalEnabled;
    }
}
