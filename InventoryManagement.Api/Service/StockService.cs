using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api.Service
{
    public class StockService
    {
        private readonly InventoryDbContext dbContext;

        public StockService(InventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AdjustStockQuantity(Guid productId, int quantity)
        {
            var stock = await dbContext.Stocks.SingleAsync(p => p.ProductId == productId);
            if (stock.Quantity < quantity)
            {
                throw new Exception($" Quantity Of Product ( {stock.ProductId}) not enough!!!");
            }
            stock.Quantity -= quantity;
            await dbContext.SaveChangesAsync();
        }
    }
}