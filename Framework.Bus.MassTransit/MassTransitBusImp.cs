using Framework.Core;
using MassTransit;

namespace Framework.Bus.MassTransit
{
    public class MassTransitBusImp : IEventBus
    {
        private readonly IBusControl bus;

        public MassTransitBusImp(IBusControl bus)
        {
            this.bus = bus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            bus.Publish(@event).GetAwaiter().GetResult();
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            await bus.Publish(@event);
        }
    }
}