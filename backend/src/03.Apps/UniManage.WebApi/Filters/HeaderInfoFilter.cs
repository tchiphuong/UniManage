using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using UniManage.Shared.Domain.Models;

namespace UniManage.WebApi.Filters
{
    /// <summary>
    /// Auto injects HeaderInfo from HttpContext into Command/Query models that have a HeaderInfo property.
    /// </summary>
    public class HeaderInfoFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;

            var username = user?.FindFirstValue(ClaimTypes.Name)
                ?? user?.FindFirstValue("username")
                ?? user?.FindFirstValue("name")
                ?? user?.FindFirstValue("preferred_username")
                ?? user?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? user?.FindFirstValue("sub");

            string? controllerName = null;
            string? actionName = null;

            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                controllerName = descriptor.ControllerName;
                actionName = descriptor.ActionName;
            }

            var headerInfo = new HeaderInfo
            {
                CorrelationId = httpContext.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? httpContext.TraceIdentifier,
                Username = username,
                UserId = user?.FindFirstValue(ClaimTypes.NameIdentifier),
                Role = user?.FindFirstValue(ClaimTypes.Role),
                IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault(),
                Language = httpContext.Request.Headers["Accept-Language"].FirstOrDefault(),
                DeviceId = httpContext.Request.Headers["Device-Id"].FirstOrDefault(),
                DeviceType = httpContext.Request.Headers["Device-Type"].FirstOrDefault(),
                AppVersion = httpContext.Request.Headers["App-Version"].FirstOrDefault(),
                SessionId = httpContext.Request.Headers["Session-Id"].FirstOrDefault(),
                AccessToken = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", ""),
                ApiName = $"{controllerName}/{actionName}"
            };

            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg != null)
                {
                    var property = arg.GetType().GetProperty(nameof(HeaderInfo));
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(arg, headerInfo);
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }
    }
}
