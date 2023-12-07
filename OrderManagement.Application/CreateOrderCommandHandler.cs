using Framework.Core;
using OrderManagement.Domain.Order;

namespace OrderManagement.Application
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Handle(CreateOrderCommand command)
        {
            var orderItems = new List<OrderItem>();
            foreach (var item in command.Items)
            {
                orderItems.Add(OrderItem.CreateOrderItem(Guid.NewGuid(), item.ProductId, item.Quantity, item.UnitPrice));
            }
            _orderRepository.Save(OrderAggregate.CreateOrder(Guid.NewGuid(), command.CustomerId, orderItems));
        }
    }

    public class AddNewItemsToOrderCommandHandler : ICommandHandler<AddNewItemsToOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public AddNewItemsToOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Handle(AddNewItemsToOrderCommand command)
        {
            var order = _orderRepository.Get(command.OrderId);
            foreach (var item in command.Items)
            {
                order.AddOrderItem(OrderItem.CreateOrderItem(Guid.NewGuid(), item.ProductId, item.Quantity, item.UnitPrice));
            }
            _orderRepository.Update(order);
        }
    }
}