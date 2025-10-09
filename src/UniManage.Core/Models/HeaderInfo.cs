namespace UniManage.Core.Models
{
    /// <summary>
    /// Model thông tin header, dùng chung cho mọi API.
    /// </summary>
    public class HeaderInfo
    {
        public string? ApiName { get; set; } // Tên API hiện tại (Controller/Action)

        // Thông tin người dùng
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? Service { get; set; }

        // Thông tin thiết bị
        public string? DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceType { get; set; }

        // Thông tin bổ sung từ header
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? AppVersion { get; set; }
        public string? Location { get; set; }
        public string? SessionId { get; set; }
        public string? Language { get; set; }

        /// <summary>
        /// Token JWT (nếu cần dùng lại)
        /// </summary>
        public string? AccessToken { get; set; }
    }
}