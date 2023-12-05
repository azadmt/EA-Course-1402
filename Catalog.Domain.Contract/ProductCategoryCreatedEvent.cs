using Framework.Core;

namespace Catalog.Domain.Contract
{
    public class ProductCategoryCreatedEvent : DomainEvent
    {
        private ProductCategoryCreatedEvent()
        {
        }

        public Guid ProductCategoryId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }

        public ProductCategoryCreatedEvent(Guid id, string name, string code)
        {
            ProductCategoryId = id;
            Name = name;
            Code = code;
        }
    }
}