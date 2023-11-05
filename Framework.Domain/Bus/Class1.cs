namespace Framework.Domain
{
    public interface IBus
    {
        void Send(ICommand command);

        void Publish(IEvent @event);
    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommand : IMessage
    {
    }

    public interface IEvent : IMessage
    {
    }

    public interface IMessage
    {
        Guid TraceId { get; }
    }
}