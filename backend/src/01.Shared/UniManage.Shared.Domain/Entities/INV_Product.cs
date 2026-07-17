using UniManage.Shared.Domain.Entities;

namespace UniManage.Shared.Domain.Entities
{
    public class INV_Product : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// JSON lưu tên đa ngôn ngữ: {"vi": "Sản phẩm", "en": "Product"}
        /// </summary>
        public string NameLocalized { get; set; } = "{}";

        public decimal Price { get; set; }
        
        public Guid CategoryId { get; set; }
        public virtual INV_ProductCategory? Category { get; set; }
    }
}
