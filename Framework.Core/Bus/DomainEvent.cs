namespace Framework.Core
{
    public interface IEvent
    {
    }

    public abstract class DomainEvent : IEvent
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreationDate { get; private set; } = DateTime.Now;
    }
}