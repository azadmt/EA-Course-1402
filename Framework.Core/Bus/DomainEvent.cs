using System.Text.Json.Serialization;

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

        [JsonInclude]
        public Guid Id { get; protected set; }

        [JsonInclude]
        public DateTime CreationDate { get; protected set; }
    }
}