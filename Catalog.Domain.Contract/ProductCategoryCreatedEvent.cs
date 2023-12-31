﻿using Framework.Core;

namespace Catalog.Domain.Contract
{
    public class ProductCategoryCreatedEvent : DomainEvent
    {
        public Guid ProductCategoryId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }

        public ProductCategoryCreatedEvent(Guid productCategoryId, string name, string code)
        {
            ProductCategoryId = productCategoryId;
            Name = name;
            Code = code;
        }
    }
}