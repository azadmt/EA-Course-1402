using Catalog.Domaim;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.EF.Mapping
{
    internal class ProductMapping : IEntityTypeConfiguration<ProductAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.ToTable("Products").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.IsActive);
            builder.Property(p => p.CategoryId);

            builder.OwnsOne(p => p.Price);
            builder.OwnsOne(p => p.Code);
        }
    }
}