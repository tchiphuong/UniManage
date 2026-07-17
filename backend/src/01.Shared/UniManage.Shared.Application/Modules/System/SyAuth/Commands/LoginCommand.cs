using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Services;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    // ===========================================
    // SECURITY-TODO (C2): ROPC Flow Deprecation
    // ===========================================
    // This handler currently uses Resource Owner Password Credentials (ROPC)
    // grant_type, which is DEPRECATED in OAuth 2.1 (RFC 9700).
    // ===========================================

    #region Command

    /// <summary>
    /// Command to perform system login.
    /// </summary>
    public sealed class LoginCommand : BaseCommand, IRequest<ApiResponse<LoginCommand.Response>>
    {
        /// <summary>
        /// User's login name.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// User's password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Unique device identifier (Mobile).
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// Device type (iOS, Android, Web).
        /// </summary>
        public string? DeviceType { get; set; }

        /// <summary>
        /// Firebase Cloud Messaging token.
        /// </summary>
        public string? FcmToken { get; set; }

        /// <summary>
        /// Successful login response data.
        /// </summary>
        public class Response
        {
            /// <summary>
            /// Access token for API authorization.
            /// </summary>
            public string AccessToken { get; set; } = string.Empty;

            /// <summary>
            /// Refresh token to renew access token.
            /// </summary>
            public string RefreshToken { get; set; } = string.Empty;

            /// <summary>
            /// Access token lifetime in seconds.
            /// </summary>
            public int ExpiresIn { get; set; }

            /// <summary>
            /// Token type (usually Bearer).
            /// </summary>
            public string TokenType { get; set; } = "Bearer";

            /// <summary>
            /// Basic user information after login.
            /// </summary>
            public UserInfo User { get; set; } = new UserInfo();
        }

        /// <summary>
        /// Compact user information.
        /// </summary>
        public class UserInfo
        {
            /// <summary>
            /// User unique identifier.
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// User unique code (Username).
            /// </summary>
            public string UserCode { get; set; } = string.Empty;

            /// <summary>
            /// User's display name.
            /// </summary>
            public string DisplayName { get; set; } = string.Empty;

            /// <summary>
            /// User's email address.
            /// </summary>
            public string? Email { get; set; }

            /// <summary>
            /// User's role code.
            /// </summary>
            public string? RoleCode { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for LoginCommand.
    /// </summary>
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username))
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_username, 50));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_password));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for system login, identity verification, and status check.
    /// </summary>
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginCommand.Response>>
    {
        private readonly IIdentityServerClient _identityClient;

        public LoginCommandHandler(IIdentityServerClient identityClient)
        {
            _identityClient = identityClient;
        }

        /// <summary>
        /// Handles the login request.
        /// </summary>
        public async Task<ApiResponse<LoginCommand.Response>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo());
            try
            {
                // 1. Validate user account status
                var (statusSuccess, statusError, userSecurity) = await AuthHelper.ValidateUserStatusAsync(request.Username, log, cancellationToken);
                if (!statusSuccess)
                {
                    return ReturnError(statusError!, "Account status check failed", log);
                }

                // 2. Authenticate via IdentityServer (ROPC flow)
                var (authSuccess, tokenData, identityError) = await _identityClient.RequestTokenAsync(request.Username, request.Password, cancellationToken);

                if (!authSuccess || tokenData == null)
                {
                    // Increment failed login count
                    await AuthHelper.HandleLoginFailureAsync(request.Username, log, cancellationToken);
                    return ReturnError(CoreResource.auth_invalidLogin, identityError ?? "IdentityServer authentication failed", log);
                }

                // 3. Login success - Reset failure count and update device info
                await AuthHelper.HandleLoginSuccessAsync(request.Username, log, cancellationToken);

                if (!string.IsNullOrEmpty(request.DeviceId))
                {
                    await AuthHelper.UpdateDeviceTokenAsync(userSecurity!.Id, request.DeviceId, request.FcmToken, request.DeviceType, log, cancellationToken);
                }

                // 4. Load full user profile using EF Core
                using (var dbContext = new Shared.Infrastructure.Database.DbContext())
                {
                    var user = await dbContext.Set<SyUsers>()
                        .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

                    if (user == null || user.Status != CoreCommon.Value.Commonstatus.Active)
                    {
                        return ReturnError(
                            user == null ? CoreResource.auth_userNotFound : CoreResource.auth_accountInactive,
                            user == null ? "User not found after successful identity check" : "Account inactivated during process", 
                            log);
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
                            UserCode = user.Username,
                            DisplayName = user.EmployeeCode ?? user.Username,
                            Email = user.Email,
                            RoleCode = user.RoleCode
                        }
                    };

                    var response = ResponseHelper.Success(responseData, CoreResource.auth_loginSuccess);
                    return FinalizeLogAndReturn(response, log);
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<LoginCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }

        /// <summary>
        /// Creates error response and standardizes logging.
        /// </summary>
        private ApiResponse<LoginCommand.Response> ReturnError(string userMsg, string logMsg, ApiLogModel log)
        {
            var errorResponse = ResponseHelper.Error<LoginCommand.Response>(userMsg);
            log.Message = logMsg;
            log.ReturnCode = errorResponse.ReturnCode;
            return errorResponse;
        }

        /// <summary>
        /// Finalizes log metadata before returning successful response.
        /// </summary>
        private ApiResponse<LoginCommand.Response> FinalizeLogAndReturn(ApiResponse<LoginCommand.Response> res, ApiLogModel log)
        {
            log.Result = res.Data;
            log.Message = res.Message;
            log.ReturnCode = res.ReturnCode;
            return res;
        }
    }

    #endregion
}









