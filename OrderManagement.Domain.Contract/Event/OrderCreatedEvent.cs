using Framework.Core;
using OrderManagement.Domain.Contract.Dto;

namespace OrderManagement.Domain.Contract
{
    public class OrderCreatedEvent : DomainEvent
    {
        public OrderCreatedEvent(Guid orderId, List<OrderItemDto> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }

        public Guid OrderId { get; private set; }

        public List<OrderItemDto> OrderItems { get; private set; }
    }
}