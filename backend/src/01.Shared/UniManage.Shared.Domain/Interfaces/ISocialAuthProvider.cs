namespace UniManage.Shared.Domain.Interfaces
{
    /// <summary>
    /// Model chﬂ+¨a th+¶ng tin ng¶¶ﬂ+•i d+¶ng lﬂ¶—y tﬂ+Ω Social Provider
    /// </summary>
    public class SocialUserProfile
    {
        public string Provider { get; set; } = string.Empty;
        public string ProviderUserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Picture { get; set; }
    }

    /// <summary>
    /// Interface chung cho c+Ìc Social Auth Provider (Google, Facebook, Apple...)
    /// </summary>
    public interface ISocialAuthProvider
    {
        /// <summary>
        /// T+¨n -Êﬂ+Ônh danh cﬂ+∫a provider (google, facebook...)
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// X+Ìc thﬂ+¶c token v+· lﬂ¶—y th+¶ng tin profile ng¶¶ﬂ+•i d+¶ng
        /// </summary>
        /// <param name="token">Token tﬂ+Ω client SDK</param>
        /// <returns>Profile ng¶¶ﬂ+•i d+¶ng nﬂ¶+u token hﬂ+˙p lﬂ+Á</returns>
        Task<SocialUserProfile?> VerifyTokenAsync(string token, CancellationToken ct = default);
    }
}

