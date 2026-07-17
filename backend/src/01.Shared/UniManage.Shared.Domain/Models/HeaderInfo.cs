using System.Text.Json.Serialization;

namespace UniManage.Shared.Domain.Models
{
    /// <summary>
    /// Model thông tin header, dùng chung cho m?i API.
    /// </summary>
    public class HeaderInfo
    {
        public string? ApiName { get; set; } // Tên API hi?n t?i (Controller/Action)

        /// <summary>
        /// ID d?nh danh duy nh?t cho m?i request (Trace ID)
        /// </summary>
        public string? CorrelationId { get; set; }

        // Thông tin ngu?i dùng
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }

        // Thông tin thi?t b?
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }

        // Thông tin b? sung t? header
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? AppVersion { get; set; }
        public string? SessionId { get; set; }
        public string? Language { get; set; }

        /// <summary>
        /// Ð? l?ch múi gi? c?a client (phút). VD: -420 cho UTC+7
        /// </summary>
        public int? ClientTimezoneOffset { get; set; }

        /// <summary>
        /// Token JWT (n?u c?n dùng l?i)
        /// </summary>
        [JsonIgnore]
        public string? AccessToken { get; set; }
    }
}
