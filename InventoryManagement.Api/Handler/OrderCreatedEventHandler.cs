using InventoryManagement.Api.Service;
using InventoryManagement.Contract;
using MassTransit;
using OrderManagement.Domain.Contract;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InventoryManagement.Api.Handler
{
    public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
    {
        private readonly StockService stockService;

        public OrderCreatedEventHandler(StockService stockService)
        {
            this.stockService = stockService;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            foreach (var item in context.Message.OrderItems)
            {
                try
                {
                    await stockService.AdjustStockQuantity(item.ProductId, item.Quantity);
                }
                catch (Exception)
                {
                    await context.Publish(new StockAdjusmentConfirmedEvent(context.Message.OrderId));
                    throw;
                }
            }
            await context.Publish(new StockAdjusmentConfirmedEvent(context.Message.OrderId));
        }
    }
}