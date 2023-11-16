using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domaim.Product
{
    public interface IProductRepository
    {
        ProductAggregate Get(Guid id);
        ProductAggregate Get(ProductCode code);
        void Save(ProductAggregate product);
        void Update(ProductAggregate product);
    }
}
