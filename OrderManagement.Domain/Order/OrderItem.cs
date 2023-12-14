using Framework.Domain;

namespace OrderManagement.Domain.Order
{
    public class OrderItem : Entity<Guid>
    {
        private OrderItem()
        {
        }

        private OrderItem(Guid id) : base(id)
        {
        }

        public static OrderItem CreateOrderItem(Guid id, Guid productId, int quantity, decimal price)
        {
            var orerItem = new OrderItem(id);
            orerItem.ProductId = productId;
            orerItem.Quantity = quantity;
            orerItem.Price = price;
            return orerItem;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public decimal Price { get; private set; }
    }
}