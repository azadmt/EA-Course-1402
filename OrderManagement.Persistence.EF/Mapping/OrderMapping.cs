using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Domain.Order;

namespace OrderManagement.Persistence.EF.Mapping
{
    internal class OrderAggregateMapping : IEntityTypeConfiguration<OrderAggregate>
    {
        public void Configure(EntityTypeBuilder<OrderAggregate> builder)
        {
            builder.ToTable("Orders").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.OrderDate);
            builder.Property(p => p.TotalPrice);
            builder.Property(p => p.CustomerId);

            builder.HasMany(p => p.OrderItems)
                .WithOne()
                .HasForeignKey("OrderAggregateId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}