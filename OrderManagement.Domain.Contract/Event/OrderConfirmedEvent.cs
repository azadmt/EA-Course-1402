using Framework.Core;

namespace OrderManagement.Domain.Contract
{
    public class OrderConfirmedEvent : DomainEvent
    {
        public OrderConfirmedEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}