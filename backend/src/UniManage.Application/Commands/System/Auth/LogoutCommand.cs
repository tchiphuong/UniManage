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
    /// Lệnh đăng xuất người dùng và hủy bỏ hiệu lực của Refresh Token
    /// </summary>
    public sealed class LogoutCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        /// <summary>
        /// Mã Refresh Token cần thu hồi (tùy chọn)
        /// </summary>
        public string? RefreshToken { get; set; }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh đăng xuất
    /// </summary>
    public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            // RefreshToken là tùy chọn, không bắt buộc kiểm tra
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh đăng xuất người dùng
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
            // Khởi tạo log nghiệp vụ
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.RefreshToken), StringHelper.MaskSensitiveData(request.RefreshToken))
                }
            };

            try
            {
                // Nếu không có refresh token, chỉ thực hiện đăng xuất phía Client
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    var clientLogoutResponse = ResponseHelper.Success(true, CoreResource.auth_logoutSuccess);
                    log.Message = "Client logout success";
                    log.ReturnCode = clientLogoutResponse.ReturnCode;
                    return clientLogoutResponse;
                }

                // Thu hồi Refresh Token trên IdentityServer
                var (success, _) = await _identityClient.RevokeTokenAsync(request.RefreshToken, ct);

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
