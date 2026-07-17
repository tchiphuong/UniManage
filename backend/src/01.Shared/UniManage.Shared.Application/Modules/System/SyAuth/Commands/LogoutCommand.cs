using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Commands
{
    #region Command

    /// <summary>
    /// L?nh dang xu?t ngu?i d�ng v� h?y b? hi?u l?c c?a Refresh Token
    /// </summary>
    public sealed class LogoutCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// M� Refresh Token c?n thu h?i (t�y ch?n)
        /// </summary>
        public string? RefreshToken { get; set; }
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho l?nh dang xu?t
    /// </summary>
    public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            // RefreshToken l� t�y ch?n, kh�ng b?t bu?c ki?m tra
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� l?nh dang xu?t ngu?i d�ng
    /// </summary>
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<bool>>
    {
        private readonly IIdentityServerClient _identityClient;

        public LogoutCommandHandler(IIdentityServerClient identityClient)
        {
            _identityClient = identityClient;
        }

        public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v?
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.RefreshToken), StringHelper.MaskSensitiveData(request.RefreshToken ?? string.Empty))
                }
            };

            try
            {
                // N?u kh�ng c� refresh token, ch? th?c hi?n dang xu?t ph�a Client
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    var clientLogoutResponse = ResponseHelper.Success(true, CoreResource.auth_logoutSuccess);
                    log.Message = "Client logout success";
                    log.ReturnCode = clientLogoutResponse.ReturnCode;
                    return clientLogoutResponse;
                }

                // Thu h?i Refresh Token tr�n IdentityServer
                var (success, _) = await _identityClient.RevokeTokenAsync(request.RefreshToken, cancellationToken);

                var response = ResponseHelper.Success(true, CoreResource.auth_logoutSuccess);
                log.Message = "Logout success";
                log.ReturnCode = response.ReturnCode;
                log.Result = success;

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}





