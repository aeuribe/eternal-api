using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("DISTRICT");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Relación con Region
            builder.HasOne(d => d.Region)
                .WithMany() // Region no tiene una lista explícita de Districts en la entidad
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.Restrict); // Evita el borrado en cascada
        }
    }
}
