using Catalog.Domaim;
using Catalog.Domaim.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.EF.Repository
{
    public class ProductAggregateRepository : IProductRepository
    {
        private readonly ProductCatalogDbContext uow;

        public ProductAggregateRepository(ProductCatalogDbContext uow)
        {
            this.uow = uow;
        }
        public ProductAggregate Get(ProductCode code)
        {
            return uow.Products.SingleOrDefault(p => p.Code == code);
        }

        public ProductAggregate Get(Guid id)
        {
            return uow.Products.SingleOrDefault(p => p.Id==id);

        }

        public void Save(ProductAggregate product)
        {
            uow.Products.Add(product);
            uow.SaveChanges();

        }

        public void Update(ProductAggregate product)
        {
            throw new NotImplementedException();
        }
    }
}
