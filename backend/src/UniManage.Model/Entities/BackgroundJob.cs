using System;

namespace UniManage.Model.Entities
{
    public class BackgroundJob
    {
        public int Id { get; set; }
        public string JobCode { get; set; } = default!;
        public string JobName { get; set; } = default!;
        public string CronExpression { get; set; } = default!;
        public bool IsDisabled { get; set; }
        public string? Description { get; set; }

        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public byte[] DataRowVersion { get; set; } = Array.Empty<byte>();
    }
}
