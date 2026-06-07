namespace UniManage.Shared.Application.Interfaces
{
    public class IdentityTokenResponse
    {
        public string access_token { get; set; } = string.Empty;
        public int expires_in { get; set; }
        public string token_type { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
    }

    public interface IIdentityServerClient
    {
        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RequestTokenAsync(
            string username, string password, CancellationToken ct = default);

        Task<(bool Success, IdentityTokenResponse? Token, string? Error)> RefreshTokenAsync(
            string refreshToken, CancellationToken ct = default);

        Task<(bool Success, string? Error)> RevokeTokenAsync(
            string refreshToken, CancellationToken ct = default);
    }
}
