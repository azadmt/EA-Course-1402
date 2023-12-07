using Catalog.Domain.Contract;
using Framework.Core;
using Framework.Domain;
using System;
using System.Diagnostics;

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
            AddChanges(new ProductActivated(Id));
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
}