using Catalog.Application.DataContract;
using Catalog.Application.DataContract.ProductCategory;
using Catalog.Domain.Contract;
using Catalog.Domain.ProductCategory;
using Framework.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Catalog.Application.ProductCategory
{
    public class ProductCategoryCommandHandler : ICommandHandler<CreateProductCategoryCommand>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IEventBus _eventBus;

        public ProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, IEventBus eventBus)
        {
            _productCategoryRepository = productCategoryRepository;
            _eventBus = eventBus;
        }

        public void Handle(CreateProductCategoryCommand command)
        {
            var productCategory = new ProductCategoryAggregate(Guid.NewGuid(), command.Code, command.Name);
            _productCategoryRepository.Save(productCategory);
        }
    }
}