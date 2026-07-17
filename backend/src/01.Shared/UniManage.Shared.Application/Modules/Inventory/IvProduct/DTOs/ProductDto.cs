namespace UniManage.Shared.Application.Modules.IvProduct.DTOs
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string NameLocalized { get; set; } = "{}";
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
