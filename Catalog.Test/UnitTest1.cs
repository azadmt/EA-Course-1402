using Catalog.Application;
using Catalog.Domaim;
using Catalog.Domaim.Product;
using Catalog.Domaim.ProductCategory;

namespace Catalog.Test
{
    public class ProductTest
    {
        [Fact]
        public void CreateProduct()
        {
            //Arrange
            var productCategoryRepo = new FakeProductCategoryRepository();
            var someCategoryName = "someCategoryName";
            var someCategoryCode = "12";
            var someCategoryId = Guid.NewGuid();
            var productRepo = new FakeProductRepository();
            productCategoryRepo.Save(new ProductCategoryAggregate(someCategoryId, someCategoryName, someCategoryCode));
            var sut = new ProductService(productRepo, productCategoryRepo);

            //Act
            var createProductDto = new Application.DataContract.CreateProductDto
            {
                Category = someCategoryId,
                CountryCode = "100",
                Price = 5000,
                ProductName = "someName"

            };
            sut.CreateProductCatalog(createProductDto);

            //Assert
            var createdProduct = productRepo.Get(new ProductCode(someCategoryCode, createProductDto.CountryCode));

            Assert.True(createdProduct != null);
            Assert.True(createProductDto.Price == createdProduct.Price.Value);

        }
    }


    internal class FakeProductCategoryRepository : IProductCategoryRepository
    {
        List<ProductCategoryAggregate> _db = new List<ProductCategoryAggregate>();
        public ProductCategoryAggregate Get(Guid id)
        {
            return _db.Single(x => x.Id == id);
        }

        public void Save(ProductCategoryAggregate productCategory)
        {
            _db.Add(productCategory);
        }

        public void Update(ProductCategoryAggregate productCategory)
        {
            throw new NotImplementedException();
        }
    }

    internal class FakeProductRepository : IProductRepository
    {
        List<ProductAggregate> _db = new List<ProductAggregate>();
        public ProductAggregate Get(ProductCode code)
        {
            return _db.Single(x => x.Code.Value == code.Value);
        }

        public void Save(ProductAggregate product)
        {
            _db.Add(product);
        }

        public void Update(ProductAggregate product)
        {
            throw new NotImplementedException();
        }
    }
}