using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("REGION");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Relación con Area
            builder.HasOne(r => r.Area)
                .WithMany() // Area no tiene una lista explícita de Regions en la entidad
                .HasForeignKey(r => r.AreaId)
                .OnDelete(DeleteBehavior.Restrict); // Evita el borrado en cascada
        }
    }
}
