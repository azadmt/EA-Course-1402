using Catalog.Domain;
using Catalog.Domain.ProductCategory;
using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.EF
{
    public class ProductCatalogDbContext : OutboxSupportApplicationDbContext
    {
        public ProductCatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<ProductCategoryAggregate> ProductCategories { get; set; }
    }
}