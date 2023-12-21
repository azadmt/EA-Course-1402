namespace InventoryManagement.Contract
{
    public class StockAdjusmentRejectedEvent
    {
        public StockAdjusmentRejectedEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}