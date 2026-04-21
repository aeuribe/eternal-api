using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class SalesRouteConfiguration : IEntityTypeConfiguration<SalesRoute>
    {
        public void Configure(EntityTypeBuilder<SalesRoute> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("SALES_ROUTE");

            // 2. Llave Primaria
            builder.HasKey(r => r.Id);

            // 3. Propiedades y Tipos de Datos
            builder.Property(r => r.Code)
                   .IsRequired()
                   .HasMaxLength(25); // Basado en el VARCHAR(25) de tu diagrama

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true); // Toda ruta nace activa

            // 4. Índices y Constraints (Reglas de Negocio)
            // ¡VITAL! Esto garantiza que a nivel de motor SQL jamás existan dos "FL-01"
            builder.HasIndex(r => r.Code)
                   .IsUnique();

            // 5. Relaciones (Foreign Keys)
            // Relación con City: Una ruta le pertenece a una ciudad
            builder.HasOne(r => r.City)
                   .WithMany() // Déjalo vacío si tu clase City no tiene un ICollection<SalesRoute>
                   .HasForeignKey(r => r.CityId)
                   .OnDelete(DeleteBehavior.Restrict); // ¡Protección! 
        }
    }
}