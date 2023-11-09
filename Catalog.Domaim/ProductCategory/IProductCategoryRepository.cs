using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domaim.ProductCategory
{
    public interface IProductCategoryRepository
    {
        ProductCategoryAggregate Get(Guid id);
        void Save(ProductCategoryAggregate productCategory);
        void Update(ProductCategoryAggregate productCategory);
    }
}
