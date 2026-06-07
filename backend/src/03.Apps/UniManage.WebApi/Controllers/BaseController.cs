using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManage.Shared.Application.Models;

namespace UniManage.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
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
            ApiName = $"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName}"
        };

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
    }
}
