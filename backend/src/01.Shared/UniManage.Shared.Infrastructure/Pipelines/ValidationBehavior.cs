using FluentValidation;
using MediatR;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.Shared.Infrastructure.Pipelines
{
    /// <summary>
    /// ValidationBehavior - T? d?ng validate t?t c? Commands/Queries b?ng FluentValidation
    /// </summary>
    /// <typeparam name="TRequest">Command ho?c Query</typeparam>
    /// <typeparam name="TResponse">ApiResponse ho?c ApiResponse</typeparam>
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
            // N?u không có validator, skip validation
            if (!_validators.Any())
            {
                return await next();
            }

            // T?o validation context
            var context = new ValidationContext<TRequest>(request);

            // Ch?y t?t c? validators song song
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Gom t?t c? l?i
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // N?u có l?i, tr? v? ApiResponse.Fail v?i errors
            if (failures.Any())
            {
                return CreateValidationErrorResponse(failures);
            }

            // Validation pass, ti?p t?c d?n Handler
            return await next();
        }

        /// <summary>
        /// T?o validation error response theo dúng chu?n ApiResponse
        /// </summary>
        private static TResponse CreateValidationErrorResponse(List<FluentValidation.Results.ValidationFailure> failures)
        {
            // Convert FluentValidation errors sang FieldError
            var fieldErrors = failures.ToFieldErrors();

            var responseType = typeof(TResponse);

            // Ki?m tra n?u TResponse là ApiResponse<T>
            if (responseType.IsGenericType)
            {
                var genericType = responseType.GetGenericTypeDefinition();

                // ApiResponse<T>
                if (genericType == typeof(ApiResponse<>))
                {
                    var dataType = responseType.GetGenericArguments()[0];
                    var apiResponseType = typeof(ApiResponse<>).MakeGenericType(dataType);

                    // T?o instance v?i ReturnCode = InvalidData
                    var response = Activator.CreateInstance(apiResponseType);

                    apiResponseType.GetProperty(nameof(ApiResponse<object>.ReturnCode))!.SetValue(response, CoreApiReturnCode.InvalidData);
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Message))!.SetValue(response, "Validation failed");
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Errors))!.SetValue(response, fieldErrors);
                    apiResponseType.GetProperty(nameof(ApiResponse<object>.Data))!.SetValue(response, null);

                    return (TResponse)response!;
                }
            }

            // Fallback: throw ValidationException n?u không match pattern
            throw new ValidationException(failures);
        }
    }
}



