namespace ProductCatalog.Domain.Product
{
    public interface IProductRepository
    {
        void Save(ProductAggregate product);

        void Update(ProductAggregate product);

        ProductAggregate Get(Guid id);
    }
}