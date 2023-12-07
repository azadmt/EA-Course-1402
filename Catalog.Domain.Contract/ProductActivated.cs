using Framework.Core;

namespace Catalog.Domain.Contract
{
    public class ProductActivated : DomainEvent
    {
        public ProductActivated(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }
    }
}