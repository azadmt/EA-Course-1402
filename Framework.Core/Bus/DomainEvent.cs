namespace Framework.Core
{
    public abstract class DomainEvent
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreationDate { get; private set; } = DateTime.Now;
    }
}