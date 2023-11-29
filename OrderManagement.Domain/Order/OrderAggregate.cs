using Ardalis.GuardClauses;
using Framework.Domain;
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

        private OrderAggregate(Guid id, Guid customerId, List<OrderItem> orderItems) : base(id)
        {
            CustomerId = customerId;
            _orderItems = orderItems;
        }

        public static OrderAggregate CreateOrder(Guid id, Guid customerId, List<OrderItem> orderItems)
        {
            Guard.Against.NullOrEmpty(orderItems, nameof(orderItems));

            return new OrderAggregate(id, customerId, orderItems);
        }

        private List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTime OrderDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid CustomerId { get; private set; }
    }
}