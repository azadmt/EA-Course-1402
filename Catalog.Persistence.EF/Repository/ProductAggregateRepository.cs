using Catalog.Domain;
using Catalog.Domain.Product;
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
            return uow.Products.SingleOrDefault(p => p.ProductCode == code);
        }

        public ProductAggregate Get(Guid id)
        {
            return uow.Products.SingleOrDefault(p => p.Id == id);
        }

        public void Save(ProductAggregate product)
        {
            uow.Products.Add(product);
            uow.SaveChanges();
        }

        public void Update(ProductAggregate product)
        {
            uow.Products.Update(product);
            uow.SaveChanges();
        }
    }
}