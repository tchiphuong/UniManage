using MediatR;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Queries
{
    #region Query

    /// <summary>
    /// Query l?y chu?i challenge ng?u nhi�n d? k� sinh tr?c h?c.
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
                // T?o m?t chu?i ng?u nhi�n (Nonce)
                // Trong th?c t? n�n luu v�o Cache/Session v?i th?i gian h?t h?n ng?n (v� d? 2 ph�t)
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




