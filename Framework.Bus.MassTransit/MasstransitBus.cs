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

        public void Publish<TEvent>(TEvent @event, string traceId = null) where TEvent : class
        {
            _bus.Publish<TEvent>(@event, x => { x.Headers.Set("TraceId", traceId); }).GetAwaiter().GetResult();
        }
    }
}