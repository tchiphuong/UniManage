using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Application.Services;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    // ===========================================
    // SECURITY-TODO (C2): ROPC Flow Deprecation
    // ===========================================
    // This handler currently uses Resource Owner Password Credentials (ROPC)
    // grant_type, which is DEPRECATED in OAuth 2.1 (RFC 9700).
    //
    // Migration plan:
    //   1. Configure IdentityServer to support Authorization Code + PKCE flow
    //   2. Update frontend to redirect to IdentityServer login page
    //   3. Backend only validates tokens — never touches credentials directly
    //   4. Remove this entire LoginCommand once migration is complete
    // ===========================================

    #region Command

    /// <summary>
    /// Login Command - Đăng nhập hệ thống
    /// </summary>
    public sealed class LoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        public class Response
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            public int ExpiresIn { get; set; }
            public string TokenType { get; set; } = "Bearer";
            public UserInfo User { get; set; } = new UserInfo();
        }

        public class UserInfo
        {
            public long Id { get; set; }
            public string UserCode { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string? RoleCode { get; set; }
        }
    }

    #endregion

    #region Validator

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

            // [SECURITY] H4 — Enforce minimum 8 chars consistent with CreateUser policy
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters");
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Login Command Handler — uses shared IIdentityServerClient + CoreLogModel
    /// </summary>
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginCommand.Response>>
    {
        private readonly IIdentityServerClient _identityClient;

        public LoginCommandHandler(IIdentityServerClient identityClient)
        {
            _identityClient = identityClient;
        }

        public async Task<ApiResponse<LoginCommand.Response>> Handle(LoginCommand request, CancellationToken ct)
        {
            // Khởi tạo log với HeaderInfo từ BaseCommand
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Username), request.Username)
                }
            };

            try
            {
                // 1. Authenticate via IdentityServer
                var (success, tokenData, error) = await _identityClient.RequestTokenAsync(
                    request.Username, request.Password, ct);

                if (!success || tokenData == null)
                {
                    var errorResponse = ResponseHelper.Error<LoginCommand.Response>("Invalid username or password");
                    log.ReturnCode = errorResponse.ReturnCode;
                    log.Message = error;
                    return errorResponse;
                }

                // 2. Get User Info from database to enrich response
                using (var dbContext = new DbContext())
                {
                    var sql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName] AS UserCode,
                            [EmployeeCode],
                            [RoleCode],
                            [Email]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var user = await dbContext.QueryFirstOrDefaultAsync<UserDto>(
                        sql, new { request.Username });

                    if (user == null)
                    {
                        var notFoundResponse = ResponseHelper.Error<LoginCommand.Response>("User data not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = "User data not found in database";
                        return notFoundResponse;
                    }

                    var responseData = new LoginCommand.Response
                    {
                        AccessToken = tokenData.access_token,
                        RefreshToken = tokenData.refresh_token,
                        ExpiresIn = tokenData.expires_in,
                        TokenType = tokenData.token_type,
                        User = new LoginCommand.UserInfo
                        {
                            Id = user.Id,
                            UserCode = user.UserCode,
                            DisplayName = user.EmployeeCode ?? user.UserCode,
                            Email = user.Email,
                            RoleCode = user.RoleCode
                        }
                    };

                    var response = ResponseHelper.Success(responseData, CoreResource.auth_loginSuccess);
                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<LoginCommand.Response>("An error occurred during login");
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }

        private class UserDto
        {
            public long Id { get; set; }
            public string UserCode { get; set; } = default!;
            public string? EmployeeCode { get; set; }
            public string? RoleCode { get; set; }
            public string? Email { get; set; }
        }
    }

    #endregion
}
