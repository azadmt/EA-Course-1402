using Framework.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Framework.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }

        private IList<DomainEvent> _changes;

        public IReadOnlyCollection<DomainEvent> GetChanges()
        {
            return new ReadOnlyCollection<DomainEvent>(_changes);
        }
    }

    public interface IAggregateRoot
    {
        IReadOnlyCollection<DomainEvent> GetChanges();
    }
}