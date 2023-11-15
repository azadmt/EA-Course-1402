using Framework.Core.Bus;

namespace Framework.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        public List<IEvent> Changes { get; private set; } = new List<IEvent>();

        public AggregateRoot(TId id) : base(id)

        {
        }
    }
}