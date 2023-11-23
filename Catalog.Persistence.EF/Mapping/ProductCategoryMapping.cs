using Catalog.Domaim.ProductCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.EF.Mapping
{
    internal class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryAggregate> builder)
        {
            builder.ToTable("ProductCategories").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.Name);
            builder.Property(p => p.Code);
        }
    }
}