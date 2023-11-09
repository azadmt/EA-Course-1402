using Catalog.Application.DataContract;
using Catalog.Domaim;
using Catalog.Domaim.Product;
using Catalog.Domaim.ProductCategory;

namespace Catalog.Application
{
    public class ProductService
    {
        private readonly IProductRepository repository;
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductService(IProductRepository repository, IProductCategoryRepository productCategoryRepository)
        {
            this.repository = repository;
            this.productCategoryRepository = productCategoryRepository;
        }

        public void CreateProductCatalog(CreateProductDto dto)
        {
            var productCategory = productCategoryRepository.Get(dto.Category);
            var price = new Price(dto.Price);
            var code = new ProductCode(productCategory.Code, dto.CountryCode);
            var product = new ProductAggregate(Guid.NewGuid(),productCategory.Id, price, code);
            repository.Save(product);
        }
    }
}