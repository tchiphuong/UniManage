using FluentValidation;
using MediatR;
using UniManage.Application.Services;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.Auth
{
    #region Command

    /// <summary>
    /// Logout Command - Đăng xuất và revoke token
    /// </summary>
    public sealed class LogoutCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Refresh Token cần revoke
        /// </summary>
        public string? RefreshToken { get; set; }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Logout Command Validator
    /// </summary>
    public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            // RefreshToken is optional
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Logout Command Handler — uses shared IIdentityServerClient + CoreLogModel
    /// </summary>
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<bool>>
    {
        private readonly IIdentityServerClient _identityClient;

        public LogoutCommandHandler(IIdentityServerClient identityClient)
        {
            _identityClient = identityClient;
        }

        public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken ct)
        {
            // Khởi tạo log với HeaderInfo từ BaseCommand
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel("HasRefreshToken", !string.IsNullOrEmpty(request.RefreshToken))
                }
            };

            try
            {
                // Nếu không có refresh token, chỉ return success (client-side logout)
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    var clientLogoutResponse = ResponseHelper.Success(true, CoreResource.auth_logoutSuccess);
                    log.Result = clientLogoutResponse;
                    log.ReturnCode = clientLogoutResponse.ReturnCode;
                    log.Message = "Client-side logout only (no refresh token)";
                    return clientLogoutResponse;
                }

                var (success, error) = await _identityClient.RevokeTokenAsync(
                    request.RefreshToken, ct);

                if (!success)
                {
                    log.Message = $"Token revocation failed: {error}";
                    // Still return success — token might already be expired/revoked
                }

                var response = ResponseHelper.Success(true, CoreResource.auth_logoutSuccess);
                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message ??= response.Message;
                return response;
            }
            catch (Exception ex)
            {
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                // Return success anyway to allow client to clear tokens
                return ResponseHelper.Success(true, CoreResource.auth_logoutSuccess);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
