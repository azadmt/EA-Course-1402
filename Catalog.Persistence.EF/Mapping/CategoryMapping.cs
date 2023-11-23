using Catalog.Domaim.ProductCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.EF.Mapping
{
    public class CategoryMapping : IEntityTypeConfiguration<ProductCategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryAggregate> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code);
            builder.Property(x => x.Name);

        }
    }
}
