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
        private readonly ProductCatalogDebContext uow;

        public ProductAggregateRepository(ProductCatalogDebContext uow)
        {
            this.uow = uow;
        }
        public ProductAggregate Get(ProductCode code)
        {
            return uow.Products.SingleOrDefault(p => p.Code == code);
        }

        public void Save(ProductAggregate product)
        {
            uow.Products.Add(product);
            //uow.SaveChanges();

        }

        public void Update(ProductAggregate product)
        {
            throw new NotImplementedException();
        }
    }
}
