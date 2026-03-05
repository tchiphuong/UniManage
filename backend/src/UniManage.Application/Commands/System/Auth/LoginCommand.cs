using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Application.Services;
using UniManage.Application.Utilities;
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
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Username)
                        .MaximumLength(50)
                        .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_username, 50));
                });

            // [SECURITY] H4 — Unified password policy via extension method
            RuleFor(x => x.Password)
                .Password(CoreResource.lbl_password);
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

            var dbContext = new DbContext(openTransaction: true);
            try
            {
                using (dbContext)
                {
                    // 1. Check account Lockout status
                    var checkLockSql = @"
                        SELECT TOP 1 [Id], [LockedUntil], [FailedLoginCount], [Status]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var userSecurity = await dbContext.QueryFirstOrDefaultAsync<UserSecurityDto>(
                        checkLockSql, new { request.Username });

                    if (userSecurity != null)
                    {
                        if (userSecurity.Status != CoreCommon.Value.Commonstatus.Active)
                        {
                            var inactiveResponse = ResponseHelper.Error<LoginCommand.Response>("Account is inactive");
                            log.ReturnCode = inactiveResponse.ReturnCode;
                            log.Message = "Attempted login to inactive account";
                            return inactiveResponse;
                        }

                        if (userSecurity.LockedUntil.HasValue && userSecurity.LockedUntil.Value > DateTime.Now)
                        {
                            var remainingMinutes = Math.Ceiling((userSecurity.LockedUntil.Value - DateTime.Now).TotalMinutes);
                            var lockedResponse = ResponseHelper.Error<LoginCommand.Response>(CoreResource.auth_accountLocked);
                            log.ReturnCode = lockedResponse.ReturnCode;
                            log.Message = $"Account is locked until {userSecurity.LockedUntil.Value}";
                            return lockedResponse;
                        }
                    }

                    // 2. Authenticate via IdentityServer
                    var (success, tokenData, error) = await _identityClient.RequestTokenAsync(
                        request.Username, request.Password, ct);

                    if (!success || tokenData == null)
                    {
                        // Update failed login count
                        var updateFailedSql = @"
                            UPDATE [dbo].[sy_users]
                            SET [FailedLoginCount] = ISNULL([FailedLoginCount], 0) + 1,
                                [LockedUntil] = CASE WHEN ISNULL([FailedLoginCount], 0) + 1 >= 5 
                                                     THEN DATEADD(MINUTE, 30, GETDATE()) 
                                                     ELSE [LockedUntil] END,
                                [UpdatedAt] = GETDATE(),
                                [UpdatedBy] = @Username
                            WHERE [UserName] = @Username";

                        await dbContext.ExecuteAsync(updateFailedSql, new { request.Username });
                        await dbContext.CommitAsync();

                        var errorResponse = ResponseHelper.Error<LoginCommand.Response>(CoreResource.auth_invalidLogin);
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = error;
                        return errorResponse;
                    }

                    // 3. Login success - Reset lockout & Get User Info
                    var sql = @"
                        UPDATE [dbo].[sy_users]
                        SET [FailedLoginCount] = 0,
                            [LockedUntil] = NULL,
                            [UpdatedAt] = GETDATE(),
                            [UpdatedBy] = @Username
                        WHERE [UserName] = @Username;

                        SELECT TOP 1
                            [Id],
                            [UserName] AS UserCode,
                            [EmployeeCode],
                            [RoleCode],
                            [Email],
                            [Status]
                        FROM [dbo].[sy_users]
                        WHERE [UserName] = @Username";

                    var user = await dbContext.QueryFirstOrDefaultAsync<UserDto>(
                        sql, new { request.Username });

                    if (user == null || user.Status != CoreCommon.Value.Commonstatus.Active)
                    {
                        await dbContext.CommitAsync();
                        var notFoundResponse = ResponseHelper.Error<LoginCommand.Response>(
                            user == null ? CoreResource.auth_userNotFound : CoreResource.auth_accountInactive);
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = user == null ? "User data not found in database" : "Inactive account login attempt";
                        return notFoundResponse;
                    }

                    await dbContext.CommitAsync();

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
                await dbContext.RollbackAsync();
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<LoginCommand.Response>(CoreResource.common_error);
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
            public string Status { get; set; } = default!;
        }

        private class UserSecurityDto
        {
            public long Id { get; set; }
            public string Status { get; set; } = default!;
            public DateTime? LockedUntil { get; set; }
            public int? FailedLoginCount { get; set; }
        }
    }

    #endregion
}
