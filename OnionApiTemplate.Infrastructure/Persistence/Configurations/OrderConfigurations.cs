using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Infrastructure.Persistence.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder
            .HasOne(o => o.Invoice)
            .WithOne(i => i.Order)
            .HasForeignKey<Invoice>(i => i.OrderId);
        }
    }
}
