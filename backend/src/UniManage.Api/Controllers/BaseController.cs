using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManage.Model.Common;

namespace UniManage.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        #region Properties

        protected string? AccessToken
        {
            get
            {
                if (Request.Headers.TryGetValue("Authorization", out var value))
                {
                    return value.ToString().Replace("Bearer ", "").Trim();
                }
                return null;
            }
        }

        protected string UserId
        {
            get
            {
                return User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                       User?.FindFirstValue("sub") ?? 
                       string.Empty;
            }
        }

        protected string Username
        {
            get
            {
                return User?.FindFirstValue(ClaimTypes.Name) ?? 
                       User?.FindFirstValue("name") ?? 
                       User?.FindFirstValue("username") ?? 
                       User?.FindFirstValue("preferred_username") ?? 
                       string.Empty;
            }
        }

        protected string Email
        {
            get
            {
                return User?.FindFirstValue(ClaimTypes.Email) ?? 
                       User?.FindFirstValue("email") ?? 
                       string.Empty;
            }
        }

        protected string Culture
        {
            get
            {
                if (Request.Headers.TryGetValue("Accept-Language", out var value))
                {
                    return value.ToString();
                }
                return "en-US";
            }
        }

        protected string? ClientIp
        {
            get
            {
                return HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
        }

        /// <summary>
        /// Lấy tên controller hiện tại
        /// </summary>
        protected string CurrentController
        {
            get
            {
                return ControllerContext.RouteData.Values["controller"]?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// Lấy tên action method hiện tại
        /// </summary>
        protected string CurrentAction
        {
            get
            {
                return ControllerContext.RouteData.Values["action"]?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// Lấy tên API dạng "Controller/Action"
        /// </summary>
        protected string ApiName
        {
            get
            {
                return $"{CurrentController}/{CurrentAction}";
            }
        }

        /// <summary>
        /// Thông tin header gom gọn một chỗ, dễ gọi lại.
        /// </summary>
        protected HeaderInfo HeaderInfo
        {
            get
            {
                return BuildHeaderInfo();
            }
        }

        #endregion

        #region Methods

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

        #endregion
    }
}
