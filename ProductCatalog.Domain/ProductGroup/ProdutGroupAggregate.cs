using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.ProductGroup
{
    public class ProdutGroupAggregate
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
    }
}