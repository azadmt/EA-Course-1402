using Framework.Core.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.ApplicationBus
{
    public class BusControl : ICommandBus, IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public BusControl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish(IEvent @event)
        {
            //_serviceProvider.GetService<IEvent>();
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }
    }
}