using Catalog.Application.DataContract;
using Catalog.Application.DataContract.ProductCategory;
using Catalog.Domain.ProductCategory;
using Framework.Core;

namespace Catalog.Application.ProductCategory
{
    public class ProductCategoryCommandHandler : ICommandHandler<CreateProductCategoryCommand>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public void Handle(CreateProductCategoryCommand command)
        {
            var productCategory = new ProductCategoryAggregate(Guid.NewGuid(), command.Code, command.Name);
            _productCategoryRepository.Save(productCategory);
        }
    }
}