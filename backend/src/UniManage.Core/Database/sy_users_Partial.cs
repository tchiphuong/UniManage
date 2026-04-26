using System.ComponentModel.DataAnnotations;

namespace UniManage.Model.Entities
{
    /// <summary>
    /// Mở rộng sy_users để hỗ trợ Social Auth (Cần cập nhật DB tương ứng)
    /// </summary>
    public partial class sy_users
    {
        /// <summary>
        /// Nhà cung cấp Social (google, facebook, apple...)
        /// </summary>
        [StringLength(50)]
        public string? SocialProvider { get; set; }

        /// <summary>
        /// ID người dùng của Social Provider
        /// </summary>
        [StringLength(150)]
        public string? SocialProviderId { get; set; }

        public int? FailedLoginCount { get; set; }
        public DateTime? LockedUntil { get; set; }
    }
}
