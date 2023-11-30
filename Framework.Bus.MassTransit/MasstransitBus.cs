using Framework.Core;
using MassTransit;

namespace Framework.Bus.MassTransit
{
    public class MasstransitBus : IEventBus
    {
        private readonly IBusControl _bus;

        public MasstransitBus(IBusControl bus)
        {
            _bus = bus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _bus.Publish(@event).GetAwaiter().GetResult();
        }
    }
}