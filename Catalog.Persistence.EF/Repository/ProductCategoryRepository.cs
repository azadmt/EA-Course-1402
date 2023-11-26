using Catalog.Domain.ProductCategory;
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
        private readonly ProductCatalogDbContext _dbcontext;

        public ProductCategoryRepository(ProductCatalogDbContext uow)
        {
            this._dbcontext = uow;
        }

        public ProductCategoryAggregate Get(Guid id)
        {
            return _dbcontext.ProductCategories.Single(c => c.Id == id);
        }

        public void Save(ProductCategoryAggregate productCategory)
        {
            _dbcontext.ProductCategories.Add(productCategory);
            _dbcontext.SaveChanges();
        }

        public void Update(ProductCategoryAggregate productCategory)
        {
            _dbcontext.ProductCategories.Update(productCategory);
            _dbcontext.SaveChanges();
        }
    }
}