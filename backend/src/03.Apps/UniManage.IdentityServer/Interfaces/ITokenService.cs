using UniManage.IdentityServer.Models;
using UniManage.IdentityServer.Services;

namespace UniManage.IdentityServer.Interfaces
{
    /// <summary>
    /// Thông tin kết quả trả về khi cấp phát token.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Chuỗi Access Token (JWT).
        /// </summary>
        public string AccessToken { get; set; } = default!;

        /// <summary>
        /// Loại token (thường là Bearer).
        /// </summary>
        public string TokenType { get; set; } = "Bearer";

        /// <summary>
        /// Thời gian sống của token tính bằng giây.
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Chuỗi Refresh Token (nếu có).
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Lỗi trả về (nếu có).
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Mô tả lỗi chi tiết (nếu có).
        /// </summary>
        public string? ErrorDescription { get; set; }
    }

    /// <summary>
    /// Giao diện dịch vụ cung cấp và quản lý JWT Token.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Cấp token cho người dùng (Password Flow, Authorization Code, v.v.).
        /// </summary>
        Task<TokenResponse> IssueTokenForUserAsync(IdentityUserModel user, ClientModel client, IEnumerable<string> requestedScopes);

        /// <summary>
        /// Cấp token cho ứng dụng (Client Credentials Flow).
        /// </summary>
        Task<TokenResponse> IssueTokenForClientAsync(ClientModel client, IEnumerable<string> requestedScopes);

        /// <summary>
        /// Cấp lại token mới dựa trên Refresh Token cũ.
        /// </summary>
        Task<TokenResponse> RenewTokenAsync(string refreshToken, ClientModel client);
    }
}
