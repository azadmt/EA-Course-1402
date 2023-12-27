using Framework.Core;
using OrderManagement.Domain.Order;

namespace OrderManagement.Application
{
    public class ConfirmOrderCommandHandler : ICommandHandler<ConfirmOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ConfirmOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Handle(ConfirmOrderCommand command)
        {
            var order = _orderRepository.Get(command.OrderId);
            order.Confirm();

            _orderRepository.Save(order);
        }
    }
}