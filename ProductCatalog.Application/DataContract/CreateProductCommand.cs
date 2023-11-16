using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.DataContract
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public Guid Category { get; set; }
        public decimal Price { get; set; }
    }
}