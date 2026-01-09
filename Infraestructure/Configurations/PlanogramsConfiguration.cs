using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class PlanogramConfiguration : IEntityTypeConfiguration<Planogram>
    {
        public void Configure(EntityTypeBuilder<Planogram> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Planograms");

            // Clave primaria
            builder.HasKey(p => p.Id);

            // Propiedades
            builder.Property(p => p.Id)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(p => p.isActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            // Si en el futuro agregas relaciones, aquí se configuran
            // Ejemplo:
            // builder.HasMany(p => p.Products)
            //        .WithOne()
            //        .HasForeignKey("PlanogramId");
        }
    }
}