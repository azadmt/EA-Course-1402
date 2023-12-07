using Framework.Core;

namespace Catalog.Domain.Contract
{
    public class ProductCreated : DomainEvent
    {
        public Guid ProductId { get; private set; }
        public Guid CategoryId { get; private set; }

        public ProductCreated(Guid productId, Guid categoryId, decimal price, string productCode, bool isActive)
        {
            ProductId = productId;
            CategoryId = categoryId;
            Price = price;
            ProductCode = productCode;
            IsActive = isActive;
        }

        public decimal Price { get; private set; }
        public string ProductCode { get; private set; }
        public bool IsActive { get; private set; }
    }
}