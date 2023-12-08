using Framework.Domain;

namespace Catalog.Domain.Contract
{
    public class ProductCategoryCreatedEvent : DomainEvent
    {
        public ProductCategoryCreatedEvent(Guid productCategoryId, string name, string Code)
        {
            ProductCategoryId = productCategoryId;
            Name = name;
            this.Code = Code;
        }
        public Guid ProductCategoryId { get; }
        public string Name { get; }
        public string Code { get; }
    }
}