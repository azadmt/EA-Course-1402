using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Order;
using OrderManagement.Domain.Order.State;

namespace OrderManagement.Persistence.EF.Mapping
{
    internal class OrderAggregateMapping : IEntityTypeConfiguration<OrderAggregate>
    {
        public void Configure(EntityTypeBuilder<OrderAggregate> builder)
        {
            builder.ToTable("Orders").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(x => x.RowVersion).IsConcurrencyToken();

            builder.Property(p => p.OrderDate);
            builder.Property(p => p.TotalPrice);
            builder.Property(p => p.CustomerId);
            builder.Property(p => p.State)
                .HasConversion(
                p => p.GetType().Name,
                p => GetOrderState(p)
                );

            builder.OwnsMany(x => x.OrderItems, oi =>
            {
                oi.ToTable("OrderItems").HasKey(x => x.Id);
                oi.Property(p => p.Id).IsRequired().ValueGeneratedNever();
                oi.WithOwner().HasForeignKey("OrderId");
                oi.Property(x => x.ProductId);
                oi.Property(x => x.Quantity);
                oi.Property(x => x.Quantity);
            });

            builder.Metadata.FindNavigation(nameof(OrderAggregate.OrderItems))
             .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        public OrderState GetOrderState(string state)
        {
            return state switch
            {
                nameof(PendingState) => new PendingState(),
                nameof(RejectState) => new RejectState(),
                nameof(PaiedState) => new PaiedState(),
                nameof(DeliverdState) => new DeliverdState(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}