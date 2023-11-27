using Framework.Core;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.ProductCategory
{
    public class ProductCategoryAggregate : AggregateRoot<Guid>
    {
        public ProductCategoryAggregate(Guid id, string name, string code) : base(id)
        {
            Name = name;
            Code = code;
            AddChanges(new ProductCategoryCreatedEvent(id, name, code));
            AddChanges(new ProductCategoryCreatedEvent(id, name, code));
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
    }

    public class ProductCategoryCreatedEvent : DomainEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }

        public ProductCategoryCreatedEvent(Guid id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
    }
}