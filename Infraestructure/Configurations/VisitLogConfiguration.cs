using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infrastructure.Persistence.Configurations
{
    public class VisitLogConfiguration : IEntityTypeConfiguration<VisitLog>
    {
        public void Configure(EntityTypeBuilder<VisitLog> builder)
        {
            builder.ToTable("visit_logs");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.VisitDate)
                   .HasColumnType("date")
                   .IsRequired();

            builder.HasOne(v => v.Store)
                   .WithMany()
                   .HasForeignKey(v => v.StoreId);

            builder.HasOne(v => v.Salesperson)
                   .WithMany()
                   .HasForeignKey(v => v.SalespersonId);
        }
    }
}
