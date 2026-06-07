using UniManage.Shared.Domain.Entities;

namespace UniManage.Modules.Inventory.Domain.Entities
{
    public class INV_ProductCategory : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// JSON lưu tên đa ngôn ngữ
        /// </summary>
        public string NameLocalized { get; set; } = "{}";

        public Guid? ParentId { get; set; }
    }
}
