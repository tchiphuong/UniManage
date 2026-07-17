using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManage.Shared.Domain.Entities
{
    /// <summary>
    /// Entity lưu trữ thông tin Public Key sinh trắc học của người dùng (Cần cập nhật DB)
    /// </summary>
    [Table("SyUserBiometrics")]
    public class SyUserBiometrics
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeviceId { get; set; } = default!;

        [Required]
        public string PublicKey { get; set; } = default!;

        [StringLength(255)]
        public string? DeviceName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
    }
}

