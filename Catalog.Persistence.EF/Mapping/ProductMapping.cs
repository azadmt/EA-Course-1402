using Catalog.Domaim;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.EF.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<ProductAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryId);
            builder.Property(x => x.IsActive);
            builder.OwnsOne(x => x.Code);
            builder.OwnsOne(x => x.Price);

        }
    }
}
