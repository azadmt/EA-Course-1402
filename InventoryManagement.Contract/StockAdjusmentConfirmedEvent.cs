namespace InventoryManagement.Contract
{
    public class StockAdjusmentConfirmedEvent
    {
        public StockAdjusmentConfirmedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
        public Guid OrderId { get; private set; }

    }
}