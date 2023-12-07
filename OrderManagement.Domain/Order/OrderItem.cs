using Framework.Domain;

namespace OrderManagement.Domain.Order
{
    public class OrderItem : Entity<Guid>
    {
        private OrderItem()
        {
        }

        private OrderItem(Guid id, Guid productId, int quantity, decimal price) : base(id)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public static OrderItem CreateOrderItem(Guid id, Guid productId, int quantity, decimal price)
        {
            return new OrderItem(id, productId, quantity, price);
        }

        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }
        public OrderAggregate Order { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}