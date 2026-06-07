namespace UniManage.Shared.Application.Interfaces
{
    /// <summary>
    /// Interface ─æ├ính dß║Ñu cho c├íc Query cß║ºn ─æ╞░ß╗úc cache tß╗▒ ─æß╗Öng qua CacheBehavior.
    /// </summary>
    public interface ICacheable
    {
        /// <summary>
        /// Cache key duy nhß║Ñt cho query n├áy
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// Thß╗¥i gian cache (ph├║t). Null = d├╣ng mß║╖c ─æß╗ïnh tß╗½ config
        /// </summary>
        int? CacheTTLMinutes { get; }
    }
}

