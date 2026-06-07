namespace UniManage.Shared.Infrastructure.Pipelines
{
    /// <summary>
    /// Interface đánh dấu cho các Query cần được cache tự động qua CacheBehavior.
    /// </summary>
    public interface ICacheable
    {
        /// <summary>
        /// Cache key duy nhất cho query này
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// Thời gian cache (phút). Null = dùng mặc định từ config
        /// </summary>
        int? CacheTTLMinutes { get; }
    }
}


