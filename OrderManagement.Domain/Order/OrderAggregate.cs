using Ardalis.GuardClauses;
using Framework.Domain;
using OrderManagement.Domain.Contract;
using OrderManagement.Domain.Order.Exception;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
            AddChanges(new OrderCreatedEvent(id));
        }

        public static OrderAggregate CreateOrder(Guid id, Guid customerId, List<OrderItem> orderItems)
        {
            Guard.Against.NullOrEmpty(orderItems, nameof(orderItems));

            var order = new OrderAggregate(id, customerId);

            foreach (var item in orderItems)
            {
                order.AddOrderItem(item);
            }
            return order;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            if (_orderItems.Select(x => x.Price).Sum() > 10000000)
                throw new OutOfOrderCapacityException();

            _orderItems.Add(orderItem);
        }

        private List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTime OrderDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid CustomerId { get; private set; }
    }
}