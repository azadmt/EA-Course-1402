using Framework.Core.Persistence;
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
            var validationResult = command.Validate();
            if (validationResult.HasError)
            {
                throw new InvalidOperationException(string.Join("\r\n", validationResult.Errors));
            }
            var handler = new LoggerCommandHandlerDecorator<TCommand>(serviceProvider.GetService<ICommandHandler<TCommand>>());

            handler.Handle(command);
        }
    }
}