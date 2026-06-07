using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;

namespace UniManage.Modules.System.Application.Queries.Auth
{
    #region Query

    /// <summary>
    /// Query lấy chuỗi challenge ngẫu nhiên để ký sinh trắc học.
    /// </summary>
    public sealed class GetBiometricChallengeQuery : BaseQuery, IRequest<ApiResponse<string>>
    {
        public string Username { get; set; } = default!;
        public string DeviceId { get; set; } = default!;
    }

    #endregion

    #region Handler

    public sealed class GetBiometricChallengeQueryHandler : IRequestHandler<GetBiometricChallengeQuery, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(GetBiometricChallengeQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Username), request.Username),
                    new LogParamModel(nameof(request.DeviceId), request.DeviceId)
                }
            };

            try
            {
                // Tạo một chuỗi ngẫu nhiên (Nonce)
                // Trong thực tế nên lưu vào Cache/Session với thời gian hết hạn ngắn (ví dụ 2 phút)
                var challenge = Guid.NewGuid().ToString("N");
                
                var response = ResponseHelper.Success(challenge, CoreResource.common_success);
                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<string>(CoreResource.common_error);
            }
        }
    }

    #endregion
}




