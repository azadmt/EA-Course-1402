using Catalog.Domaim;
using Catalog.Domaim.ProductCategory;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.EF
{
    public class ProductCatalogDebContext : DbContext
    {
        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<ProductCategoryAggregate> ProductCategories { get; set; }

    }
}