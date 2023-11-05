namespace ProductCatalog.Domain.Product
{
    public class ProductAggregate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ProductCode Code { get; private set; }
        public Price Price { get; private set; }
    }
}