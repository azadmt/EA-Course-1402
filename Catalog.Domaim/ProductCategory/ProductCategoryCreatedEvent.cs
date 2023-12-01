using Framework.Domain;

namespace Catalog.Domaim.ProductCategory
{
    public class ProductCategoryCreatedEvent : DomainEvent
    {
        public ProductCategoryCreatedEvent(string name,string Code)
        {
            Name = name;
            this.Code = Code;
        }

        public string Name { get; }
        public string Code { get; }
    }
}