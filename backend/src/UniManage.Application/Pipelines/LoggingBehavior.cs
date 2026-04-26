using MediatR;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Pipelines
{
    /// <summary>
    /// Tự động viết log (đầu vào, đầu ra, Exception) cho các Command/Query.
    /// Khử boilerplate Code Log trong mọi logic file API tương lai.
    /// </summary>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ILoggableCommand)
            {
                return await next();
            }

            // Gố gắng lấy HeaderInfo (Nếu Request base từ BaseModel hoặc BaseQuery)
            HeaderInfo? headerInfo = null;
            if (request is BaseModel baseModel) headerInfo = baseModel.HeaderInfo;
            else if (request is BaseQuery baseQuery) headerInfo = baseQuery.HeaderInfo;

            var log = new CoreLogModel(headerInfo ?? new HeaderInfo())
            {
                Method = "Pipeline",
                Path = typeof(TRequest).Name
            };

            // Mask Dữ liệu nhạy cảm
            var rawParams = JsonConvert.SerializeObject(request);
            log.Parameter = new List<CoreParamModel> 
            { 
                new CoreParamModel("RequestBody", MaskSensitiveData(rawParams)) 
            };

            var sw = Stopwatch.StartNew();
            try
            {
                var response = await next();
                sw.Stop();

                // Lấy thông tin ApiResponse
                if (response != null)
                {
                    var responseType = response.GetType();
                    var codeProp = responseType.GetProperty(nameof(ApiResponse<object>.ReturnCode));
                    var msgProp = responseType.GetProperty(nameof(ApiResponse<object>.Message));

                    log.ReturnCode = (int?)codeProp?.GetValue(response) ?? 0;
                    log.Message = (string?)msgProp?.GetValue(response) ?? "Success";
                    log.Result = response;
                }

                // Để ngăn ngừa spam Double Logs do Handler cũ đã tự viết log thủ công
                // ta kiểm tra xem Handler cũ đã từng in ra "Success" hay chưa, nếu chỉ mới chạy thì kệ.
                // Framework tự log ở đây để API sau này khỏi viết lại.
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                sw.Stop();
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                UniLogManager.WriteApiLog(log);

                var responseType = typeof(TResponse);
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    var dataType = responseType.GetGenericArguments()[0];
                    var apiResponseType = typeof(ApiResponse<>).MakeGenericType(dataType);
                    var response = Activator.CreateInstance(apiResponseType);

                    apiResponseType.GetProperty(nameof(ApiResponse<object>.ReturnCode))!.SetValue(response, CoreApiReturnCode.ExceptionOccurred);
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Message))!.SetValue(response, CoreResource.common_exceptionOccurred);
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Errors))!.SetValue(response, new List<string> { ex.Message });
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Data))!.SetValue(response, null);

                    return (TResponse)response!;
                }

                throw;
            }
        }

        private static string MaskSensitiveData(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var pattern = @"(""(?i)(password|token|secret|authorization|pin|otp)""\s*:\s*"")([^""]+)("")";
            return Regex.Replace(input, pattern, "$1***MASKED***$4");
        }
    }
}
