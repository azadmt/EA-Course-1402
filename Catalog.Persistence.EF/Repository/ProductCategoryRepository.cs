using Catalog.Domaim.ProductCategory;
using Framework.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.EF.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ProductCatalogDbContext dbContext;

        public ProductCategoryRepository(ProductCatalogDbContext dbContext)
        {
            
            this.dbContext = dbContext;
        }
        public ProductCategoryAggregate Get(Guid id)
        {
            return dbContext.ProductCategories.Single(p=> p.Id==id);
        }

        public void Save(ProductCategoryAggregate productCategory)
        {
            dbContext.Add(productCategory);
            dbContext.SaveChanges();
        }

        public void Update(ProductCategoryAggregate productCategory)
        {
            throw new NotImplementedException();
        }
    }
}
