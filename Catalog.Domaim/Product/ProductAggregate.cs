using Framework.Domain;
using System;

namespace Catalog.Domaim
{
    public class ProductAggregate : Entity<Guid>
    {
        public ProductAggregate(Guid id,Guid catId, Price price,ProductCode productCode) : base(id)
        {
            Price = price;
            Code = productCode;
            CategoryId=catId;
        }

        public Guid CategoryId{ get; private set; }
        public Price  Price{ get; private set; }
        public ProductCode Code{ get; private set; }
    }
}
