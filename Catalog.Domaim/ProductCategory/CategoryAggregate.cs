using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domaim.ProductCategory
{
    public class ProductCategoryAggregate : AggregateRoot<Guid>
    {
        private ProductCategoryAggregate()
        {

        }
        public ProductCategoryAggregate(Guid id, string name, string code) : base(id)
        {
            Name = name;
            Code = code;
            _changes.Add(new ProductCategoryCreatedEvent(Name, Code));
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
    }
}
