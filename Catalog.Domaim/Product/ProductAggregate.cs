using Framework.Core;
using Framework.Domain;
using System;

namespace Catalog.Domain
{
    public class ProductAggregate : AggregateRoot<Guid>
    {
        private ProductAggregate()
        {
        }

        public ProductAggregate(Guid id, Guid categoryId, Price price, ProductCode productCode) : base(id)
        {
            Price = price;
            ProductCode = productCode;
            CategoryId = categoryId;
            AddChanges(new ProductCreated(id, categoryId, price.Value, productCode.Value, IsActive));
        }

        public void Active()
        {
            IsActive = true;
        }

        public void DeActive()
        {
            IsActive = false;
        }

        public Guid CategoryId { get; private set; }
        public Price Price { get; private set; }
        public ProductCode ProductCode { get; private set; }
        public bool IsActive { get; private set; }
    }

    public class ProductCreated : DomainEvent
    {
        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; }

        public ProductCreated(Guid id, Guid categoryId, decimal price, string productCode, bool isActive)
        {
            Id = id;
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