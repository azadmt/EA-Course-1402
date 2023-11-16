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
        private readonly IUnitOfWork _uow;

        public ProductCategoryRepository(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public ProductCategoryAggregate Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(ProductCategoryAggregate productCategory)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductCategoryAggregate productCategory)
        {
            throw new NotImplementedException();
        }
    }
}
