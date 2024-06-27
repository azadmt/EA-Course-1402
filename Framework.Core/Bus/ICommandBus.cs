namespace Framework.Core
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event, string traceId = null) where TEvent : class;
    }
}