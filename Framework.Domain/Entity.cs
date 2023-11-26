namespace Framework.Domain
{
    public abstract class Entity<TId>
    {
        protected Entity()
        { }

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
}