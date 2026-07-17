namespace UniManage.Shared.Domain.Interfaces
{
    /// <summary>
    /// Interface -Ê+Ình dﬂ¶—u cho c+Ìc Query cﬂ¶∫n -Ê¶¶ﬂ+˙c cache tﬂ+¶ -Êﬂ+÷ng qua CacheBehavior.
    /// </summary>
    public interface ICacheable
    {
        /// <summary>
        /// Cache key duy nhﬂ¶—t cho query n+·y
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// Thﬂ+•i gian cache (ph+¶t). Null = d+¶ng mﬂ¶+c -Êﬂ+Ônh tﬂ+Ω config
        /// </summary>
        int? CacheTTLMinutes { get; }
    }
}

