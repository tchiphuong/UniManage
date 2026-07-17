namespace UniManage.Shared.Application.Modules.IvProduct.Models
{
    public class IvProductModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameLocalized { get; set; } = "{}";
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
