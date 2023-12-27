using Framework.Core;
using Framework.Core.Domain;
using OrderManagement.Domain.Order;

namespace OrderManagement.Application
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IGuidProvider _guidProvider;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IGuidProvider guidProvider)
        {
            _orderRepository = orderRepository;
            _guidProvider = guidProvider;
        }

        public void Handle(CreateOrderCommand command)
        {
            var order = OrderAggregate.CreateOrder(_guidProvider.NewGuid(), command.CustomerId, command.Items);

            _orderRepository.Save(order);
        }
    }
}