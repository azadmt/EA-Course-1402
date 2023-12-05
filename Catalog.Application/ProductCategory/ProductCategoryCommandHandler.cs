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
            //_productCategoryRepository.Save(productCategory);

            foreach (var item in productCategory.GetChanges())
            {
                var json = JsonConvert.SerializeObject(item);

                var t12 = Type.GetType(typeof(ProductCategoryCreatedEvent).AssemblyQualifiedName);

                // var t12 = Type.GetType("Catalog.Domain.Contract.ProductCategoryCreatedEvent");
                //Activator.CreateInstance(t11);
                object t12o = Activator.CreateInstance(t12, true);
                var ev = JsonConvert.DeserializeObject(json, t12, new JsonSerializerSettings
                {
                    ContractResolver = new NonPublicPropertiesResolver()
                });
                // JsonConvert.PopulateObject(json, t12o);
                _eventBus.Publish(ev);
                // _eventBus.Publish(item);
            }
        }
    }

    public class NonPublicPropertiesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo pi)
            {
                prop.Readable = (pi.GetMethod != null);
                prop.Writable = (pi.SetMethod != null);
            }
            return prop;
        }
    }
}