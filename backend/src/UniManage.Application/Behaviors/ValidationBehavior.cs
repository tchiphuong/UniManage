using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniManage.Core.Models;

namespace UniManage.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var errors = failures.Select(f => f.ErrorMessage).ToList();

                // Check if TResponse is ApiResponse<T>
                var responseType = typeof(TResponse);
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    var failMethod = responseType.GetMethod("Fail", new[] { typeof(List<string>) });
                    var result = failMethod?.Invoke(null, new object[] { errors });
                    return result as TResponse ?? throw new InvalidOperationException("Failed to create validation response");
                }

                // Check if TResponse is PagedResponse<T>
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(PagedResponse<>))
                {
                    var failMethod = responseType.GetMethod("Fail", new[] { typeof(List<string>) });
                    var result = failMethod?.Invoke(null, new object[] { errors });
                    return result as TResponse ?? throw new InvalidOperationException("Failed to create validation response");
                }

                throw new ValidationException("Validation failed but response type is not supported");
            }

            return await next();
        }
    }
}
