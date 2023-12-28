using Framework.Core;
using InventoryManagement.Contract;
using MassTransit;
using OrderManagement.Application;

namespace OrderManagement.Worker.EventHandler
{
    internal class StockAdjusmentConfirmedEventHandler : IConsumer<StockAdjusmentConfirmedEvent>
    {
        private readonly ICommandBus _commandBus;

        public StockAdjusmentConfirmedEventHandler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public Task Consume(ConsumeContext<StockAdjusmentConfirmedEvent> context)
        {
            //Order-> Approved
            // publish event
            _commandBus.Send(new ConfirmOrderCommand() { OrderId = context.Message.OrderId });
            return Task.CompletedTask;
        }
    }
}