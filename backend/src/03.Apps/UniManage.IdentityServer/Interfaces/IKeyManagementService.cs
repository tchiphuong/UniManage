using Microsoft.IdentityModel.Tokens;

namespace UniManage.IdentityServer.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý các khóa mã hóa/ký JWT (RSA).
    /// </summary>
    public interface IKeyManagementService
    {
        /// <summary>
        /// Lấy đối tượng khóa bảo mật (SecurityKey) dùng để ký JWT.
        /// </summary>
        RsaSecurityKey GetSigningKey();

        /// <summary>
        /// Trả về tập hợp các khóa JSON Web (JWKS) dành cho public endpoint discovery.
        /// </summary>
        JsonWebKeySet GetJwks();
    }
}
