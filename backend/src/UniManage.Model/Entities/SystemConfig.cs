using System;

namespace UniManage.Model.Entities
{
    public class SystemConfig
    {
        public int Id { get; set; }
        public string ConfigCode { get; set; } = default!;
        public string ConfigName { get; set; } = default!;
        public string? ConfigValue { get; set; }
        public string DataType { get; set; } = "STRING"; // STRING, INT, BOOL, JSON
        public string? Description { get; set; }
        public bool IsSystem { get; set; }

        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public byte[] DataRowVersion { get; set; } = Array.Empty<byte>();
    }
}
