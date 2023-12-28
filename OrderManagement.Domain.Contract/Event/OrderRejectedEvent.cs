using Framework.Core;

namespace OrderManagement.Domain.Contract
{
    public class OrderRejectedEvent : DomainEvent
    {
        public OrderRejectedEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }

    }
}