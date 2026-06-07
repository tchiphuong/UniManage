using UniManage.Shared.Domain.Entities;

namespace UniManage.Modules.System.Domain.Entities
{
    public class SYS_FileStorage : BaseEntity
    {
        public string OriginalName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public long SizeBytes { get; set; }
        
        /// <summary>
        /// SHA256 checksum để chống trùng lặp file
        /// </summary>
        public string Checksum { get; set; } = string.Empty;
    }
}

