using UniManage.Shared.Domain.Entities;

namespace UniManage.Modules.System.Domain.Entities
{
    public class SYS_FileReference : BaseEntity
    {
        public Guid FileId { get; set; }
        public virtual SYS_FileStorage? File { get; set; }
        
        /// <summary>
        /// ID của đối tượng nghiệp vụ (VD: ProductId, EmployeeId)
        /// </summary>
        public Guid EntityId { get; set; }
        
        /// <summary>
        /// Tên của entity nghiệp vụ (VD: "INV_Product", "HR_Employee")
        /// </summary>
        public string EntityName { get; set; } = string.Empty;
        
        /// <summary>
        /// Mục đích sử dụng (VD: "Avatar", "Attachment", "Document")
        /// </summary>
        public string Purpose { get; set; } = string.Empty;
    }
}

