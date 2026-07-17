using MediatR;
using System.Transactions;
using UniManage.Shared.Domain.Models;

namespace UniManage.Shared.Infrastructure.Pipelines
{
    /// <summary>
    /// T? d?ng dóng b?c các Command vào m?t Database Transaction.
    /// Ch? các Request implement ITransactionalCommand m?i b? ?nh hu?ng.
    /// Trái v?i DbContext transaction thông thu?ng (b? gi?i h?n trong 1 bi?n), 
    /// TransactionScope bao ph? luôn toàn b? ti?n trình.
    /// </summary>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // B? qua n?u Command không yêu c?u Transaction
            if (request is not ITransactionalCommand)
            {
                return await next();
            }

            // M? TransactionScope v?i AsyncFlowEnabled (b?t bu?c cho await/async)
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            
            // Ch?y logic c?a Handler
            var response = await next();

            // N?u không có exception, t? d?ng commit
            scope.Complete();

            return response;
        }
    }
}


