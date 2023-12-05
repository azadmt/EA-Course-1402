using Catalog.Application.DataContract;
using Catalog.Domaim.ProductCategory;
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.ProductCategory
{
    public class ProductCategoryCommandHandler : ICommandHandler<CreateProductCategoryCommand>
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IEventBus eventBus;

        public ProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, IEventBus eventBus)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.eventBus = eventBus;
        }

        public void Handle(CreateProductCategoryCommand command)
        {
            var pg = new ProductCategoryAggregate(Guid.NewGuid(), command.Name, command.Code);
            productCategoryRepository.Save(pg);
            foreach (var item in pg.GetChanges())
            {
                eventBus.PublishAsync(item).GetAwaiter().GetResult();//Move to Message Broker
            }
        }
    }
}