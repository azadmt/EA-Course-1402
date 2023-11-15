namespace Framework.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; private set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        private Entity()
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if ((obj as Entity<TId>) is null) return false;
            return Id.Equals((obj as Entity<TId>).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}