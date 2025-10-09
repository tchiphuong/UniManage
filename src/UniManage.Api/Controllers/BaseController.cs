using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManage.Core.Models;

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
        /// Lấy tên controller hiện tại
        /// </summary>
        protected string CurrentController =>
            ControllerContext.RouteData.Values["controller"]?.ToString() ?? string.Empty;

        /// <summary>
        /// Lấy tên action method hiện tại
        /// </summary>
        protected string CurrentAction =>
            ControllerContext.RouteData.Values["action"]?.ToString() ?? string.Empty;

        /// <summary>
        /// Lấy tên API dạng "Controller/Action"
        /// </summary>
        protected string ApiName => $"{CurrentController}/{CurrentAction}";

        /// <summary>
        /// Thông tin header gom gọn một chỗ, dễ gọi lại.
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

                IpAddress = ClientIp,
                AccessToken = AccessToken,
                ApiName = ApiName,
                Language = headers.TryGetValue("Accept-Language", out var lang) ? lang.ToString() : null,
                UserAgent = headers.TryGetValue("User-Agent", out var ua) ? ua.ToString() : null,
                AppVersion = headers.TryGetValue("App-Version", out var appVer) ? appVer.ToString() : null,
                DeviceId = headers.TryGetValue("Device-Id", out var deviceId) ? deviceId.ToString() : null,
                DeviceName = headers.TryGetValue("Device-Name", out var deviceName) ? deviceName.ToString() : null,
                DeviceType = headers.TryGetValue("Device-Type", out var deviceType) ? deviceType.ToString() : null,
                SessionId = headers.TryGetValue("Session-Id", out var sessionId) ? sessionId.ToString() : null,
                Location = headers.TryGetValue("X-Location", out var location) ? location.ToString() : null,
                Service = headers.TryGetValue("X-Service", out var service) ? service.ToString() : null,
            };
        }
    }
}