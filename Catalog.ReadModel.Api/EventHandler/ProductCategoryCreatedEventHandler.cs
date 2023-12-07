using Catalog.Domain.Contract;
using MassTransit;

namespace Catalog.ReadModel.Api.EventHandler
{
    public class ProductCategoryCreatedEventHandler : IConsumer<ProductCategoryCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ProductCategoryCreatedEvent> context)
        {
            Console.WriteLine($"Handle message :{context.Message.ProductCategoryId} - {context.Message.Code}");
        }
    }
}