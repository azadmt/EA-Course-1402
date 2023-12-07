using Framework.Core;

namespace OrderManagement.Domain.Contract
{
    public class OrderCreatedEvent : DomainEvent
    {
        public OrderCreatedEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}