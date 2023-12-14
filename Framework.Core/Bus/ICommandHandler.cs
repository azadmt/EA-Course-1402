using Framework.Core.Persistence;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Framework.Core
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }

    public class LoggerCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private ICommandHandler<TCommand> _handler;

        public LoggerCommandHandlerDecorator(ICommandHandler<TCommand> handler)
        {
            this._handler = handler;
        }

        public void Handle(TCommand command)
        {
            Console.WriteLine(JsonConvert.SerializeObject(command));
            _handler.Handle(command);
        }
    }
}