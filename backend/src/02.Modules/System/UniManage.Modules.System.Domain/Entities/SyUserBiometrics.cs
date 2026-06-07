using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManage.Modules.System.Domain
{
    /// <summary>
    /// Entity lÆ°u trá»¯ thÃ´ng tin Public Key sinh tráº¯c há»c cá»§a ngÆ°á»i dÃ¹ng (Cáº§n cáº­p nháº­t DB)
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

