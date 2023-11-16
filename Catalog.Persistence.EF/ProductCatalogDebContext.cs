using Catalog.Domaim;
using Catalog.Domaim.ProductCategory;
using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.EF
{
    public class ProductCatalogDbContext : ApplicationDbContext
    {
        public ProductCatalogDbContext()
        {

        }
        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<ProductCategoryAggregate> ProductCategories { get; set; }

    }
}