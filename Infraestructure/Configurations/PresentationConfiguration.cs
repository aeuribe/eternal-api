using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class PresentationConfiguration : IEntityTypeConfiguration<Presentation>
    {
        public void Configure(EntityTypeBuilder<Presentation> builder)
        {
            builder.ToTable("PRESENTATION");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.GenericCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Volume)
                .HasPrecision(18, 2);

            builder.Property(p => p.Unit)
                .HasMaxLength(10);

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // ==========================================
            // RELACIONES (El pegamento de tu arquitectura)
            // ==========================================

            // 1. Relación HACIA ARRIBA: Muchas Presentaciones pertenecen a 1 Familia
            builder.HasOne(p => p.Family)
                .WithMany() // <--- ¡SE DEJA VACÍO! Así le dices que Family no tiene la lista
                .HasForeignKey(p => p.FamilyId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}