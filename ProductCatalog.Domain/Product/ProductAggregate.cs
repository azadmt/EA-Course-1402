using Framework.Domain;
using ProductCatalog.Domain.ProductGroup;

namespace ProductCatalog.Domain.Product
{
    public class ProductAggregate : AggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public ProductAggregate(Guid id, string name, ProductCode code, Price price, Guid category) : base(id)
        {
            Name = name;
            Code = code;
            Price = price;
            Category = category;
        }

        public Guid Category { get; private set; }
        public ProductCode Code { get; private set; }
        public Price Price { get; private set; }

        public static ProductAggregate Create(Guid id, string name, decimal price, ProdutGroupAggregate produtGroup)
        {
            ProductAggregate productAggregate = new ProductAggregate(id, name, new ProductCode(produtGroup.Code, "", 1), new Price(price), produtGroup.Id);

            return productAggregate;
        }
    }
}