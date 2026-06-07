namespace UniManage.Shared.Application.Interfaces
{
    /// <summary>
    /// Model chß╗⌐a th├┤ng tin ng╞░ß╗¥i d├╣ng lß║Ñy tß╗½ Social Provider
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
    /// Interface chung cho c├íc Social Auth Provider (Google, Facebook, Apple...)
    /// </summary>
    public interface ISocialAuthProvider
    {
        /// <summary>
        /// T├¬n ─æß╗ïnh danh cß╗ºa provider (google, facebook...)
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// X├íc thß╗▒c token v├á lß║Ñy th├┤ng tin profile ng╞░ß╗¥i d├╣ng
        /// </summary>
        /// <param name="token">Token tß╗½ client SDK</param>
        /// <returns>Profile ng╞░ß╗¥i d├╣ng nß║┐u token hß╗úp lß╗ç</returns>
        Task<SocialUserProfile?> VerifyTokenAsync(string token, CancellationToken ct = default);
    }
}

