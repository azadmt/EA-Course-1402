using Framework.Domain;
using System;

namespace Catalog.Domaim
{
    public class ProductAggregate : Entity<Guid>
    {
        private  ProductAggregate()
        {

        }
        public ProductAggregate(Guid id,Guid catId, Price price,ProductCode productCode) : base(id)
        {
            Price = price;
            Code = productCode;
            CategoryId=catId;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void DeActive()
        {
            IsActive = false;
        }

        public Guid CategoryId{ get; private set; }
        public Price  Price{ get; private set; }
        public ProductCode Code{ get; private set; }
        public bool IsActive { get; private set; }


    }
}
