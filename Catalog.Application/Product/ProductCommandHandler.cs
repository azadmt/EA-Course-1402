using Catalog.Application.DataContract;
using Catalog.Application.DataContract.Product;
using Catalog.Domain;
using Catalog.Domain.Product;
using Catalog.Domain.ProductCategory;
using Framework.Core;
using Framework.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Product
{
    public class CreateProductCommandHandler :
        ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository repository;
        private readonly IProductCategoryRepository productCategoryRepository;

        public CreateProductCommandHandler(
            IProductRepository repository,
            IProductCategoryRepository productCategoryRepository)
        {
            this.repository = repository;
            this.productCategoryRepository = productCategoryRepository;
        }

        public void Handle(CreateProductCommand command)
        {
            var productCategory = productCategoryRepository.Get(command.Category);
            var price = new Price(command.Price);
            var code = new ProductCode(productCategory.Code, command.CountryCode);
            var product = new ProductAggregate(Guid.NewGuid(), productCategory.Id, price, code);
            repository.Save(product);
        }
    }
}