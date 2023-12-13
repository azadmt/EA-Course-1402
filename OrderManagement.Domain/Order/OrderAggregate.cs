using Ardalis.GuardClauses;
using Framework.Domain;
using OrderManagement.Domain.Contract;
using OrderManagement.Domain.Order.Exception;
using OrderManagement.Domain.Contract.Dto;
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
            _orderItems.Add(OrderItem.CreateOrderItem(Guid.NewGuid(), Id, productId, quantity, unitPrice));
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
    }
}