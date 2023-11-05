namespace Framework.Core.Bus
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public interface IEventBus
    {
        void Publish(IEvent @event);
    }
}