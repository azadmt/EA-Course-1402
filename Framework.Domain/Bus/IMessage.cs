namespace Framework.Core.Bus
{
    public interface IMessage
    {
        Guid TraceId { get; }
    }
}