using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {

        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("INVOICE");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.Total)
                .HasDefaultValue(0);

            builder.HasOne(e => e.POD)
                .WithOne()
                .HasForeignKey<Invoice>(e => e.PodId);

            builder.HasOne(e => e.Order)
                .WithOne()
                .HasForeignKey<Invoice>(e => e.OrderId);
        }
    }
}
