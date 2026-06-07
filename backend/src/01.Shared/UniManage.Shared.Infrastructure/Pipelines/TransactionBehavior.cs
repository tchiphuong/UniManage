using MediatR;
using System.Transactions;
using UniManage.Shared.Application.Models;

namespace UniManage.Shared.Infrastructure.Pipelines
{
    /// <summary>
    /// Tự động đóng bọc các Command vào một Database Transaction.
    /// Chỉ các Request implement ITransactionalCommand mới bị ảnh hưởng.
    /// Trái với DbContext transaction thông thường (bị giới hạn trong 1 biến), 
    /// TransactionScope bao phủ luôn toàn bộ tiến trình.
    /// </summary>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Bỏ qua nếu Command không yêu cầu Transaction
            if (request is not ITransactionalCommand)
            {
                return await next();
            }

            // Mở TransactionScope với AsyncFlowEnabled (bắt buộc cho await/async)
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            
            // Chạy logic của Handler
            var response = await next();

            // Nếu không có exception, tự động commit
            scope.Complete();

            return response;
        }
    }
}


