using InventoryManagement.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var stock1 = new Stock { Id = 1, ProductId = new Guid(), Quantity = 1000 };
            var stock2 = new Stock { Id = 2, ProductId = new Guid(), Quantity = 10000 };
            var stock3 = new Stock { Id = 3, ProductId = new Guid(), Quantity = 500 };

            modelBuilder.Entity<Stock>().HasData(stock1);
            modelBuilder.Entity<Stock>().HasData(stock2);
            modelBuilder.Entity<Stock>().HasData(stock3);
        }
    }
}