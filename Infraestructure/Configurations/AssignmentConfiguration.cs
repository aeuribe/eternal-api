using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infrastructure.Persistence.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("ASSIGNMENT");

            builder.HasKey(a => a.Id);

            // Regla de Negocio: 1 tienda pertenece a 1 sola ruta
            builder.HasIndex(a => a.StoreId)
                   .IsUnique()
                   .HasDatabaseName("IX_Assignment_StoreId_Unique");

            // --- RELACIONES SEGURAS ---

            // 1. Relación con la Ruta (Territorio)
            builder.HasOne(a => a.SalesRoute)
                   .WithMany()
                   .HasForeignKey(a => a.SalesRouteId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict); // ¡Protección! No borra en cascada

            // 2. Relación con la Tienda
            builder.HasOne(a => a.Store)
                   .WithMany()
                   .HasForeignKey(a => a.StoreId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict); // ¡Protección!
        }
    }
}