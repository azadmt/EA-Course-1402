using Framework.Core;
using OrderManagement.Domain.Order;

namespace OrderManagement.Application
{
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
                order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);
            }
            _orderRepository.Update(order);
        }
    }

    public class RemoveItemsFromOrderCommandHandler : ICommandHandler<RemoveItemsFromOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveItemsFromOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Handle(RemoveItemsFromOrderCommand command)
        {
            var order = _orderRepository.Get(command.OrderId);

            order.RemoveItem(command.ItemsId.ToArray());

            _orderRepository.Update(order);
        }
    }
}