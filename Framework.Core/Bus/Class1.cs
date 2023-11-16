using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public interface ICommand
    {
        bool IsValid();
    }

    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }

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