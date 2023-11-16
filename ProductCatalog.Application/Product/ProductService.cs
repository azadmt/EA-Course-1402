using ProductCatalog.Application.DataContract;
using ProductCatalog.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Product
{
    internal class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(CreateProductCommand createProductCommand)
        {
            var product = new ProductAggregate();
        }
    }
}