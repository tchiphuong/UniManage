using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UniManage.Core.Utilities;

namespace UniManage.Application.Pipelines
{
    /// <summary>
    /// Pipeline behavior tự động cache kết quả cho các Query được đánh dấu ICacheable.
    /// Chỉ áp dụng cho Read queries, không cache Commands.
    /// </summary>
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Chỉ cache nếu request implement ICacheable
            if (request is not ICacheable cacheable)
            {
                return await next();
            }

            // Kiểm tra cache hit
            var cachedResponse = await CacheHelper.GetAsync<TResponse>(cacheable.CacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            // Cache miss -> gọi handler và lưu kết quả
            var response = await next();
            
            if (response != null)
            {
                await CacheHelper.SetAsync(cacheable.CacheKey, response, cacheable.CacheTTLMinutes);
            }

            return response;
        }
    }
}
