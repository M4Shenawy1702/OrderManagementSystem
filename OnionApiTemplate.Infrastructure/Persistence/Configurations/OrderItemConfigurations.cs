using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Infrastructure.Persistence.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>

    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
             .Property(oi => oi.Discount)
             .HasPrecision(18, 2);

            builder
             .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2);
        }
    }
}
