using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Auth
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
        public async Task<ApiResponse<string>> Handle(GetBiometricChallengeQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Username), request.Username),
                    new CoreParamModel(nameof(request.DeviceId), request.DeviceId)
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
