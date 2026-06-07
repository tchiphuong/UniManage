using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.Shared.Infrastructure.Pipelines
{
    /// <summary>
    /// ValidationBehavior - Tự động validate tất cả Commands/Queries bằng FluentValidation
    /// </summary>
    /// <typeparam name="TRequest">Command hoặc Query</typeparam>
    /// <typeparam name="TResponse">ApiResponse hoặc ApiResponse</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Nếu không có validator, skip validation
            if (!_validators.Any())
            {
                return await next();
            }

            // Tạo validation context
            var context = new ValidationContext<TRequest>(request);

            // Chạy tất cả validators song song
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Gom tất cả lỗi
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // Nếu có lỗi, trả về ApiResponse.Fail với errors
            if (failures.Any())
            {
                return CreateValidationErrorResponse(failures);
            }

            // Validation pass, tiếp tục đến Handler
            return await next();
        }

        /// <summary>
        /// Tạo validation error response theo đúng chuẩn ApiResponse
        /// </summary>
        private static TResponse CreateValidationErrorResponse(List<FluentValidation.Results.ValidationFailure> failures)
        {
            // Convert FluentValidation errors sang FieldError
            var fieldErrors = failures.ToFieldErrors();

            var responseType = typeof(TResponse);

            // Kiểm tra nếu TResponse là ApiResponse<T>
            if (responseType.IsGenericType)
            {
                var genericType = responseType.GetGenericTypeDefinition();

                // ApiResponse<T>
                if (genericType == typeof(ApiResponse<>))
                {
                    var dataType = responseType.GetGenericArguments()[0];
                    var apiResponseType = typeof(ApiResponse<>).MakeGenericType(dataType);

                    // Tạo instance với ReturnCode = InvalidData
                    var response = Activator.CreateInstance(apiResponseType);

                    apiResponseType.GetProperty(nameof(ApiResponse<object>.ReturnCode))!.SetValue(response, CoreApiReturnCode.InvalidData);
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Message))!.SetValue(response, "Validation failed");
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Errors))!.SetValue(response, fieldErrors);
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Data))!.SetValue(response, null);

                    return (TResponse)response!;
                }
            }

            // Fallback: throw ValidationException nếu không match pattern
            throw new ValidationException(failures);
        }
    }
}



