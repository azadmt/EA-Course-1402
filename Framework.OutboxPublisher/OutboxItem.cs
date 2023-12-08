namespace Framework.OutboxPublisher
{
    public class OutboxItem
    {
        public string EventType { get; set; }
        public string EventBody { get; set; }
        public Guid EventId { get; set; }
        public long Id { get; set; }
    }
}