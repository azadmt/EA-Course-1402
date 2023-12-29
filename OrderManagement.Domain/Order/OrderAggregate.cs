using Framework.Domain;
using OrderManagement.Domain.Contract;
using OrderManagement.Domain.Contract.Dto;
using OrderManagement.Domain.Order.State;

namespace OrderManagement.Domain.Order
{
    public class OrderAggregate : AggregateRoot<Guid>
    {
        private OrderAggregate()
        {
        }

        private OrderAggregate(Guid id, Guid customerId) : base(id)
        {
            CustomerId = customerId;
            State = new PendingState();
        }

        public static OrderAggregate CreateOrder(Guid id, Guid customerId, List<OrderItemDto> orderItems)
        {
            var order = new OrderAggregate(id, customerId);
            foreach (var item in orderItems)
            {
                order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);
            }
            order.AddChanges(new OrderCreatedEvent(id, orderItems));
            return order;
        }

        public void AddOrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if(State.CanEdit)
            _orderItems.Add(OrderItem.CreateOrderItem(Guid.NewGuid(), productId, quantity, unitPrice));
        }

        public void Confirm()
        {           
            State = State.Confirmed();
            AddChanges(new OrderConfirmedEvent(Id));
        }

        public void Reject()
        {          
            State = State.Reject();
            AddChanges(new OrderRejectedEvent(Id));
        }

        public void Deliver()
        {
            State = State.Deliverd();
           // AddChanges(new OrderRejectedEvent(Id));
        }

        public void RemoveItem(Guid[] orderItemIds)
        {
            _orderItems.RemoveAll(x => orderItemIds.Contains(x.Id));
        }

        protected List<OrderItem> _orderItems = new List<OrderItem>();

        public IEnumerable<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTime OrderDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderState State { get; private set; }  
    }
}