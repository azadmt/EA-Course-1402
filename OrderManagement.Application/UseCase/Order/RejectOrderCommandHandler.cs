using Framework.Core;
using OrderManagement.Domain.Order;

namespace OrderManagement.Application
{
    public class RejectOrderCommandHandler : ICommandHandler<RejectOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;


        public RejectOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Handle(RejectOrderCommand command)
        {
            var order = _orderRepository.Get(command.OrderId);
            order.Reject();

            _orderRepository.Save(order);
        }
    }
}