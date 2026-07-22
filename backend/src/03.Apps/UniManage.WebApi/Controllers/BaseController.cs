using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        /// <summary>
        /// Lấy Username từ JWT claims
        /// </summary>
        protected string? Username => User?.FindFirstValue(ClaimTypes.Name)
            ?? User?.FindFirstValue("preferred_username")
            ?? User?.FindFirstValue("sub");

        /// <summary>
        /// Tạo HeaderInfo từ HttpContext cho mỗi request
        /// </summary>
#pragma warning disable S6932 // Use model binding instead of accessing the raw request data
        protected HeaderInfo HeaderInfo => new HeaderInfo
        {
            CorrelationId = HttpContext.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? HttpContext.TraceIdentifier,
            Username = Username,
            UserId = User?.FindFirstValue(ClaimTypes.NameIdentifier),
            Role = User?.FindFirstValue(ClaimTypes.Role),
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            UserAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
            Language = HttpContext.Request.Headers["Accept-Language"].FirstOrDefault(),
            DeviceId = HttpContext.Request.Headers["Device-Id"].FirstOrDefault(),
            DeviceType = HttpContext.Request.Headers["Device-Type"].FirstOrDefault(),
            AppVersion = HttpContext.Request.Headers["App-Version"].FirstOrDefault(),
            SessionId = HttpContext.Request.Headers["Session-Id"].FirstOrDefault(),
            AccessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", ""),
            ApiName = $"{ControllerContext.ActionDescriptor.AttributeRouteInfo?.Template ?? HttpContext.Request.Path.Value?.TrimStart('/')}_{HttpContext.Request.Method}".Replace("/", "_").Replace("{", "").Replace("}", "").ToLower()
        };
#pragma warning restore S6932

        /// <summary>
        /// Lấy User ID dạng Guid từ JWT claims
        /// </summary>
        protected Guid? CurrentUserId
        {
            get
            {
                var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(userId, out var id) ? id : null;
            }
        }

        /// <summary>
        /// Chuyển đổi ApiResponse thành ActionResult với HTTP Status Code tương ứng
        /// </summary>
        protected ActionResult<ApiResponse<T>> ToActionResult<T>(ApiResponse<T> response)
        {
            if (response == null) return StatusCode(500);

            return response.ReturnCode switch
            {
                0 => Ok(response),
                400 => BadRequest(response),
                401 => Unauthorized(response),
                403 => StatusCode(403, response),
                404 => NotFound(response),
                _ => StatusCode(500, response)
            };
        }
    }
}
