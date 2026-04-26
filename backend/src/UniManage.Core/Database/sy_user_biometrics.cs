using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManage.Model.Entities
{
    /// <summary>
    /// Entity lưu trữ thông tin Public Key sinh trắc học của người dùng (Cần cập nhật DB)
    /// </summary>
    [Table("sy_user_biometrics")]
    public class sy_user_biometrics
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeviceId { get; set; }

        [Required]
        public string PublicKey { get; set; }

        [StringLength(255)]
        public string? DeviceName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
    }
}
