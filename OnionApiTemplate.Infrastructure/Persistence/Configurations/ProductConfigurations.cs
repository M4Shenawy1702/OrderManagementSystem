using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Infrastructure.Persistence.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
            .Property(p => p.Price)
            .HasPrecision(18, 2);
        }
    }
}
