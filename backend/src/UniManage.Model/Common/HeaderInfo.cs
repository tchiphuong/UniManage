using System.Text.Json.Serialization;

namespace UniManage.Model.Common
{
    /// <summary>
    /// Model thông tin header, dùng chung cho mọi API.
    /// </summary>
    public class HeaderInfo
    {
        public string? ApiName { get; set; } // Tên API hiện tại (Controller/Action)

        /// <summary>
        /// ID định danh duy nhất cho mỗi request (Trace ID)
        /// </summary>
        public string? CorrelationId { get; set; }

        // Thông tin người dùng
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }

        // Thông tin thiết bị
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }

        // Thông tin bổ sung từ header
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? AppVersion { get; set; }
        public string? SessionId { get; set; }
        public string? Language { get; set; }

        /// <summary>
        /// Độ lệch múi giờ của client (phút). VD: -420 cho UTC+7
        /// </summary>
        public int? ClientTimezoneOffset { get; set; }

        /// <summary>
        /// Token JWT (nếu cần dùng lại)
        /// </summary>
        [JsonIgnore]
        public string? AccessToken { get; set; }
    }
}