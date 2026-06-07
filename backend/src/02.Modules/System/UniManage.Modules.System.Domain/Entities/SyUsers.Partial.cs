using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniManage.Modules.HumanResource.Domain;

namespace UniManage.Modules.System.Domain
{
    /// <summary>
    /// Mở rộng SyUsers để hỗ trợ Social Auth (Cần cập nhật DB tương ứng)
    /// </summary>
    public partial class SyUsers
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

        /// <summary>
        /// Thông tin nhân viên liên kết
        /// </summary>
        [ForeignKey("EmployeeCode")]
        public virtual HrEmployees? HrEmployees { get; set; }
    }
}
