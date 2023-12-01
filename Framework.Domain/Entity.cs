using System;
using System.Collections.Generic;

namespace Framework.Domain
{
    public abstract class Entity<TId>
    {
        public Entity()
        {

        }
        public TId Id { get; private set; }

        public Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj as Entity<TId> is null) return false;
            return Id.Equals(((Entity<TId>)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

    public interface IAggregateRoot
    {
        IReadOnlyCollection<DomainEvent> GetChanges();
    }

    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
        protected List<DomainEvent> _changes=new();

        protected AggregateRoot() { }
        public AggregateRoot(TKey id):base(id)
        {

        }
        public IReadOnlyCollection<DomainEvent> GetChanges()
        {
            return _changes.AsReadOnly();
        }
    }
}
