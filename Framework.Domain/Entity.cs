namespace Framework.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; private set; }

        public Entity(TId id)
        {
            Id = id;
        }
    }
}
