using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Lớp tiện ích tập trung xử lý cache Redis cho toàn hệ thống.
    /// Cung cấp các phương thức Get/Set/Remove với serialization tự động.
    /// </summary>
    public static class CacheHelper
    {
        private static ConnectionMultiplexer? _redis;
        private static IDatabase? _db;
        private static string _instanceName = string.Empty;
        private static int _defaultTTLMinutes = UniManage.Shared.Infrastructure.Constant.CacheTimeConstant.Normal;
        private static bool _enabled = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// Khởi tạo kết nối Redis từ cấu hình
        /// </summary>
        public static void Initialize(IConfiguration config)
        {
            try
            {
                var redisConfig = config.GetSection("Redis");
                _enabled = redisConfig.GetValue<bool>("Enabled", false);

                if (!_enabled) return;

                _instanceName = redisConfig["InstanceName"] ?? string.Empty;
                _defaultTTLMinutes = redisConfig.GetValue<int>("DefaultTTLMinutes", UniManage.Shared.Infrastructure.Constant.CacheTimeConstant.Normal);
                var connectionString = redisConfig["ConnectionString"];

                if (string.IsNullOrEmpty(connectionString))
                {
                    _enabled = false;
                    return;
                }

                lock (_lock)
                {
                    if (_redis == null || !_redis.IsConnected)
                    {
                        var options = ConfigurationOptions.Parse(connectionString);
                        options.AbortOnConnectFail = false;
                        _redis = ConnectionMultiplexer.Connect(options);
                        _db = _redis.GetDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.UniLogManager.WriteApiLog(nameof(CacheHelper), "Error initializing Redis: " + ex.Message, null, true, ex);
                _enabled = false;
            }
        }

        /// <summary>
        /// Lấy dữ liệu từ cache, nếu miss thì gọi factory và set cache
        /// </summary>
        public static async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, int? ttlMinutes = null)
        {
            if (!_enabled || _db == null)
            {
                return await factory();
            }

            var fullKey = BuildKey(key);
            try
            {
                var cachedValue = await _db.StringGetAsync(fullKey);
                if (cachedValue.HasValue)
                {
                    return JsonConvert.DeserializeObject<T>(cachedValue!);
                }
            }
            catch
            {
                // Ignore cache read errors, fallback to factory
            }

            var value = await factory();
            
            if (value != null)
            {
                try
                {
                    var serializedValue = JsonConvert.SerializeObject(value);
                    var expiry = TimeSpan.FromMinutes(ttlMinutes ?? _defaultTTLMinutes);
                    await _db.StringSetAsync(fullKey, serializedValue, expiry);
                }
                catch
                {
                    // Ignore cache write errors
                }
            }

            return value;
        }

        /// <summary>
        /// Lấy dữ liệu từ cache
        /// </summary>
        public static async Task<T?> GetAsync<T>(string key)
        {
            if (!_enabled || _db == null) return default;

            try
            {
                var fullKey = BuildKey(key);
                var cachedValue = await _db.StringGetAsync(fullKey);
                
                if (cachedValue.HasValue)
                {
                    return JsonConvert.DeserializeObject<T>(cachedValue!);
                }
            }
            catch
            {
                // Ignore cache errors
            }

            return default;
        }

        /// <summary>
        /// Ghi dữ liệu vào cache
        /// </summary>
        public static async Task SetAsync<T>(string key, T value, int? ttlMinutes = null)
        {
            if (!_enabled || _db == null || value == null) return;

            try
            {
                var fullKey = BuildKey(key);
                var serializedValue = JsonConvert.SerializeObject(value);
                var expiry = TimeSpan.FromMinutes(ttlMinutes ?? _defaultTTLMinutes);
                await _db.StringSetAsync(fullKey, serializedValue, expiry);
            }
            catch
            {
                // Ignore cache errors
            }
        }

        /// <summary>
        /// Xóa một key cache cụ thể
        /// </summary>
        public static async Task RemoveAsync(string key)
        {
            if (!_enabled || _db == null) return;

            try
            {
                await _db.KeyDeleteAsync(BuildKey(key));
            }
            catch
            {
                // Ignore cache errors
            }
        }

        /// <summary>
        /// Xóa tất cả cache theo pattern (Sử dụng với cẩn trọng)
        /// Không khả dụng trên một số Redis cluster configuration.
        /// </summary>
        public static async Task RemoveByPatternAsync(string pattern)
        {
            if (!_enabled || _redis == null || _db == null) return;

            try
            {
                var fullPattern = BuildKey(pattern);
                var endpoints = _redis.GetEndPoints();
                
                foreach (var endpoint in endpoints)
                {
                    var server = _redis.GetServer(endpoint);
                    if (server.IsConnected && !server.IsReplica)
                    {
                        var keys = server.Keys(pattern: fullPattern, pageSize: 250);
                        foreach (var key in keys)
                        {
                            await _db.KeyDeleteAsync(key);
                        }
                    }
                }
            }
            catch
            {
                // Ignore cache errors
            }
        }

        /// <summary>
        /// Tạo cache key chuẩn hóa kết hợp với InstanceName
        /// </summary>
        public static string BuildKey(string key)
        {
            if (key.StartsWith(_instanceName)) return key;
            return _instanceName + key;
        }

        /// <summary>
        /// Tạo cache key chuẩn hóa theo quy tắc: {module}:{entity}:{identifier}
        /// </summary>
        public static string BuildKey(string module, string entity, string? identifier = null)
        {
            var key = $"{module}:{entity}";
            if (!string.IsNullOrEmpty(identifier))
            {
                key += $":{identifier}";
            }
            return key;
        }
    }
}




