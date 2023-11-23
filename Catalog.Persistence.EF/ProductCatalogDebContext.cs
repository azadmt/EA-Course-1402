using Catalog.Domaim;
using Catalog.Domaim.ProductCategory;
using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.EF
{
    public class ProductCatalogDbContext : ApplicationDbContext
    {
        public ProductCatalogDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ProductCatalogDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<ProductCategoryAggregate> ProductCategories { get; set; }

    }
}