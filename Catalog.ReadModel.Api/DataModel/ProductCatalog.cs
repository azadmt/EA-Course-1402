namespace Catalog.ReadModel.Api.DataModel
{
    public class ProductCatalog
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; private set; }
        public decimal Price { get; private set; }
        public string Code { get; private set; }
        public bool IsActive { get; private set; }
    }
}