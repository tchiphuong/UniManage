using Dapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Models;

namespace UniManage.Application.Commands.System.Auth
{
    /// <summary>
    /// Login Command - Đăng nhập hệ thống
    /// </summary>
    public sealed class LoginCommand : IRequest<ApiResponse<LoginCommand.Response>>
    {
        public string Username { get; init; } = default!;
        public string Password { get; init; } = default!;

        /// <summary>
        /// Login Response với JWT tokens
        /// </summary>
        public sealed class Response
        {
            public string AccessToken { get; init; } = default!;
            public string RefreshToken { get; init; } = default!;
            public int ExpiresIn { get; init; }
            public string TokenType { get; init; } = "Bearer";
            public UserInfo User { get; init; } = default!;
        }

        public sealed class UserInfo
        {
            public int Id { get; init; }
            public string UserCode { get; init; } = default!;
            public string DisplayName { get; init; } = default!;
            public string? Email { get; init; }
            public string? RoleCode { get; init; }
        }
    }

    /// <summary>
    /// Login Command Validator
    /// </summary>
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }

    /// <summary>
    /// Login Command Handler
    /// </summary>
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginCommand.Response>>
    {
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(
            ILogger<LoginCommandHandler> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ApiResponse<LoginCommand.Response>> Handle(
            LoginCommand request,
            CancellationToken ct)
        {
            try
            {
                using (var dbContext = new DbContext())
                {
                    // Query user from database
                    var sql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName] AS UserCode,
                            [Password],
                            [EmployeeCode],
                            [RoleCode],
                            [Email],
                            [Status]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username
                            AND [Status] = 1";

                    var user = await dbContext.connection.QueryFirstOrDefaultAsync<UserDto>(
                        sql,
                        new { request.Username });

                    if (user == null)
                    {
                        _logger.LogWarning("Login failed: User {Username} not found", request.Username);
                        return ApiResponse<LoginCommand.Response>.Fail("Invalid username or password");
                    }

                    // Verify password (simple comparison for demo - should use hashing in production)
                    if (user.Password != request.Password)
                    {
                        _logger.LogWarning("Login failed: Invalid password for {Username}", request.Username);
                        return ApiResponse<LoginCommand.Response>.Fail("Invalid username or password");
                    }

                    // Generate JWT tokens
                    var accessToken = GenerateAccessToken(user);
                    var refreshToken = GenerateRefreshToken();
                    var expiresIn = 3600; // 1 hour

                    var response = new LoginCommand.Response
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        ExpiresIn = expiresIn,
                        TokenType = "Bearer",
                        User = new LoginCommand.UserInfo
                        {
                            Id = user.Id,
                            UserCode = user.UserCode,
                            DisplayName = user.EmployeeCode ?? user.UserCode,
                            Email = user.Email,
                            RoleCode = user.RoleCode
                        }
                    };

                    _logger.LogInformation("User {Username} logged in successfully", request.Username);
                    return ApiResponse<LoginCommand.Response>.Success(response, "Login successful");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Username}", request.Username);
                return ApiResponse<LoginCommand.Response>.Fail("An error occurred during login");
            }
        }

        private string GenerateAccessToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "UniManage_Secret_Key_2025_At_Least_32_Characters_Long";
            var issuer = jwtSettings["Issuer"] ?? "UniManage.Api";
            var audience = jwtSettings["Audience"] ?? "UniManage.Client";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserCode),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", user.RoleCode ?? "User"),
                new Claim("employeeCode", user.EmployeeCode ?? "")
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private class UserDto
        {
            public int Id { get; set; }
            public string UserCode { get; set; } = default!;
            public string Password { get; set; } = default!;
            public string? EmployeeCode { get; set; }
            public string? RoleCode { get; set; }
            public string? Email { get; set; }
            public int Status { get; set; }
        }
    }
}
