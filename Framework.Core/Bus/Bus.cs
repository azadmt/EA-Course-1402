using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core
{
    public class Bus : ICommandBus
    {
        private readonly IServiceProvider serviceProvider;

        public Bus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = serviceProvider.GetService<ICommandHandler<TCommand>>();

            handler.Handle(command);
        }
    }
}