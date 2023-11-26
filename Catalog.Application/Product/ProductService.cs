using Catalog.Application.DataContract;
using Catalog.Application.DataContract.Product;
using Catalog.Domain;
using Catalog.Domain.Product;
using Catalog.Domain.ProductCategory;

namespace Catalog.Application
{
    //public class ProductService
    //{
    //    private readonly IProductRepository repository;
    //    private readonly IProductCategoryRepository productCategoryRepository;

    //    public ProductService(IProductRepository repository, IProductCategoryRepository productCategoryRepository)
    //    {
    //        this.repository = repository;
    //        this.productCategoryRepository = productCategoryRepository;
    //    }

    //    public void CreateProductCatalog(CreateProductCommand dto)
    //    {
    //        var productCategory = productCategoryRepository.Get(dto.Category);
    //        var price = new Price(dto.Price);
    //        var code = new ProductCode(productCategory.Code, dto.CountryCode);
    //        var product = new ProductAggregate(Guid.NewGuid(),productCategory.Id, price, code);
    //        repository.Save(product);
    //        //publish event
    //    }

    //    public void ActiveProduct(Guid id)
    //    {
    //        var product=repository.Get(id);
    //        product.Active();
    //        repository.Update(product);
    //    }

    //    public void DeActiveProduct(Guid id)
    //    {
    //        var product = repository.Get(id);
    //        product.DeActive();
    //        repository.Update(product);
    //    }
    //}
}