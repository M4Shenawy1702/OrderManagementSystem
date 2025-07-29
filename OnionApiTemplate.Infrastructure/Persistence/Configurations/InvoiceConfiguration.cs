using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Infrastructure.Persistence.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder
                .Property(i => i.TotalAmount)
                .HasPrecision(18, 2);
        }
    }
}
