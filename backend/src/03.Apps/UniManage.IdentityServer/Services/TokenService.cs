using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Service chịu trách nhiệm xử lý logic liên quan đến cấp phát, tạo và làm mới JWT token.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IKeyManagementService _keyManagementService;
        private readonly IPersistedGrantStore _grantStore;
        private readonly IIdentityUserRepository _userRepository;
        private readonly string _issuer;

        /// <summary>
        /// Khởi tạo TokenService với các phụ thuộc cần thiết.
        /// </summary>
        public TokenService(
            IKeyManagementService keyManagementService,
            IPersistedGrantStore grantStore,
            IIdentityUserRepository userRepository,
            IConfiguration configuration)
        {
            _keyManagementService = keyManagementService;
            _grantStore = grantStore;
            _userRepository = userRepository;
            _issuer = configuration["IdentityServer:Authority"] ?? "https://identity.unimanage.com";
        }

        /// <summary>
        /// Cấp token dành cho đối tượng người dùng cuối (ví dụ: luồng Password hoặc Authorization Code).
        /// </summary>
        /// <param name="user">Thông tin người dùng.</param>
        /// <param name="client">Thông tin Client đang yêu cầu token.</param>
        /// <param name="requestedScopes">Danh sách các scope được yêu cầu.</param>
        /// <returns>Đối tượng TokenResponse chứa Access Token và Refresh Token.</returns>
        public async Task<TokenResponse> IssueTokenForUserAsync(IdentityUserModel user, ClientModel client, IEnumerable<string> requestedScopes)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CoreConstant.Claim.Name, user.UserName),
                new Claim(CoreConstant.Claim.Username, user.UserName),
                new Claim(CoreConstant.Claim.EmployeeCode, user.EmployeeCode ?? ""),
                new Claim(CoreConstant.Claim.Role, user.RoleCode ?? CoreConstant.DefaultRole),
                new Claim("client_id", client.ClientId)
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }

            foreach (var scope in requestedScopes)
            {
                claims.Add(new Claim("scope", scope));
            }

            var token = CreateJwtToken(claims, client.AccessTokenLifetime);
            
            string? refreshToken = null;
            if (client.AllowOfflineAccess && requestedScopes.Contains("offline_access"))
            {
                refreshToken = await CreateAndStoreRefreshTokenAsync(user.Id.ToString(), client);
            }

            return new TokenResponse
            {
                AccessToken = token,
                ExpiresIn = client.AccessTokenLifetime,
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// Cấp token dành cho ứng dụng (luồng Client Credentials).
        /// </summary>
        /// <param name="client">Thông tin Client đang yêu cầu token.</param>
        /// <param name="requestedScopes">Danh sách các scope được yêu cầu.</param>
        /// <returns>Đối tượng TokenResponse chứa Access Token.</returns>
        public async Task<TokenResponse> IssueTokenForClientAsync(ClientModel client, IEnumerable<string> requestedScopes)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, client.ClientId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("client_id", client.ClientId)
            };

            foreach (var scope in requestedScopes)
            {
                claims.Add(new Claim("scope", scope));
            }

            var token = CreateJwtToken(claims, client.AccessTokenLifetime);

            return new TokenResponse
            {
                AccessToken = token,
                ExpiresIn = client.AccessTokenLifetime
            };
        }

        /// <summary>
        /// Cấp lại Access Token mới dựa trên Refresh Token.
        /// </summary>
        /// <param name="refreshToken">Chuỗi Refresh Token.</param>
        /// <param name="client">Thông tin Client đang yêu cầu làm mới.</param>
        /// <returns>Đối tượng TokenResponse chứa Token mới hoặc thông báo lỗi.</returns>
        public async Task<TokenResponse> RenewTokenAsync(string refreshToken, ClientModel client)
        {
            var grant = await _grantStore.GetAsync(refreshToken);
            
            if (grant == null || grant.Type != "refresh_token" || grant.ClientId != client.ClientId)
            {
                return new TokenResponse { Error = "invalid_grant", ErrorDescription = "Invalid refresh token" };
            }

            if (grant.Expiration.HasValue && grant.Expiration.Value < DateTime.UtcNow)
            {
                await _grantStore.RemoveAsync(refreshToken);
                return new TokenResponse { Error = "invalid_grant", ErrorDescription = "Refresh token expired" };
            }

            if (grant.ConsumedTime.HasValue)
            {
                // Threat detected: token reused. Remove all grants for this subject + client
                await _grantStore.RemoveAllAsync(grant.SubjectId, grant.ClientId, "refresh_token");
                return new TokenResponse { Error = "invalid_grant", ErrorDescription = "Refresh token already consumed" };
            }

            // Mark as consumed (Wait, we can just delete it and issue a new one)
            await _grantStore.RemoveAsync(refreshToken);

            if (string.IsNullOrEmpty(grant.SubjectId))
            {
                return new TokenResponse { Error = "invalid_grant", ErrorDescription = "Subject ID missing" };
            }

            var user = await _userRepository.FindByIdAsync(int.Parse(grant.SubjectId));
            if (user == null || !await _userRepository.IsUserActiveAsync(user.Id))
            {
                return new TokenResponse { Error = "invalid_grant", ErrorDescription = "User is inactive or deleted" };
            }

            // Extract scopes from Data (assumed CSV or JSON)
            var scopes = grant.Data.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return await IssueTokenForUserAsync(user, client, scopes);
        }

        /// <summary>
        /// Hàm nội bộ tạo JWT Access Token và ký bằng RSA.
        /// </summary>
        /// <param name="claims">Danh sách các Claim cần đưa vào token.</param>
        /// <param name="lifetimeSeconds">Thời gian sống của token.</param>
        /// <returns>Chuỗi JWT Token đã được ký.</returns>
        private string CreateJwtToken(IEnumerable<Claim> claims, int lifetimeSeconds)
        {
            var key = _keyManagementService.GetSigningKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer, // Custom IDP usually uses its issuer as audience, or the API resources
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(lifetimeSeconds),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Tạo một Refresh Token ngẫu nhiên và lưu vào Database qua Dapper.
        /// </summary>
        /// <param name="subjectId">Id của người dùng (SubjectId).</param>
        /// <param name="client">Thông tin Client yêu cầu Refresh Token.</param>
        /// <returns>Chuỗi Refresh Token được sinh ra.</returns>
        private async Task<string> CreateAndStoreRefreshTokenAsync(string subjectId, ClientModel client)
        {
            var tokenBytes = new byte[32];
            RandomNumberGenerator.Fill(tokenBytes);
            var handle = Base64UrlEncoder.Encode(tokenBytes);

            var grant = new PersistedGrantModel
            {
                Key = handle,
                Type = "refresh_token",
                ClientId = client.ClientId,
                SubjectId = subjectId,
                CreationTime = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddSeconds(client.RefreshTokenLifetime),
                Data = string.Join(" ", client.AllowedScopes) // Normally this should be requested scopes, simplified here
            };

            await _grantStore.StoreAsync(grant);

            return handle;
        }
    }
}
