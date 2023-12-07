namespace Framework.Core
{
    public interface IEvent
    {
    }

    public abstract class DomainEvent : IEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}