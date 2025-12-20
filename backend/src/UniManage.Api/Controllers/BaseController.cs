using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string AccessToken =>
            Request.Headers.TryGetValue("Authorization", out var value)
                ? value.ToString().Replace("Bearer ", "").Trim()
                : null;

        protected string UserId =>
            User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        protected string Username =>
            User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        protected string Email =>
            User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        protected string Culture =>
            Request.Headers.TryGetValue("Accept-Language", out var value)
                ? value.ToString()
                : "en-US";

        protected string ClientIp =>
            HttpContext?.Connection?.RemoteIpAddress?.ToString();

        /// <summary>
        /// L?y t�n controller hi?n t?i
        /// </summary>
        protected string CurrentController =>
            ControllerContext.RouteData.Values["controller"]?.ToString() ?? string.Empty;

        /// <summary>
        /// L?y t�n action method hi?n t?i
        /// </summary>
        protected string CurrentAction =>
            ControllerContext.RouteData.Values["action"]?.ToString() ?? string.Empty;

        /// <summary>
        /// L?y t�n API d?ng "Controller/Action"
        /// </summary>
        protected string ApiName => $"{CurrentController}/{CurrentAction}";

        /// <summary>
        /// Th�ng tin header gom g?n m?t ch?, d? g?i l?i.
        /// </summary>
        protected HeaderInfo HeaderInfo => BuildHeaderInfo();

        private HeaderInfo BuildHeaderInfo()
        {
            var headers = Request.Headers;

            return new HeaderInfo
            {
                UserId = UserId,
                Username = Username,
                Role = User?.FindFirstValue(ClaimTypes.Role),

                CorrelationId = headers.TryGetValue("X-Correlation-Id", out var cid) ? cid.ToString() : HttpContext.TraceIdentifier,
                ClientTimezoneOffset = headers.TryGetValue("X-Timezone-Offset", out var offset) && int.TryParse(offset, out var val) ? val : null,

                IpAddress = ClientIp,
                AccessToken = AccessToken,
                ApiName = ApiName,
                Language = headers.TryGetValue("Accept-Language", out var lang) ? lang.ToString() : null,
                UserAgent = headers.TryGetValue("User-Agent", out var ua) ? ua.ToString() : null,
                AppVersion = headers.TryGetValue("App-Version", out var appVer) ? appVer.ToString() : null,
                DeviceId = headers.TryGetValue("Device-Id", out var deviceId) ? deviceId.ToString() : null,
                DeviceType = headers.TryGetValue("Device-Type", out var deviceType) ? deviceType.ToString() : null,
                SessionId = headers.TryGetValue("Session-Id", out var sessionId) ? sessionId.ToString() : null,
            };
        }
    }
}
