using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eternal_api.Domain.Entities;

namespace eternal_api.Infrastructure.Configurations
{
    public class HistPriceConfiguration : IEntityTypeConfiguration<HistPrice>
    {
        public void Configure(EntityTypeBuilder<HistPrice> builder)
        {
            builder.ToTable("HIST_PRICE");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.StartDate)
                .IsRequired();

            builder.Property(p => p.EndDate)
                .IsRequired(false);
        }
    }
}
