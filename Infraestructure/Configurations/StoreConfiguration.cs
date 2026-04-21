using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("STORE");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.StoreNumber)
                .IsRequired()
                .HasMaxLength(50); // Puedes ajustar este tamaño según el formato de tus tiendas

            builder.Property(s => s.ZoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ZipCode)
                .IsRequired()
                .HasMaxLength(20);

            // Se quita el IsRequired porque en la entidad es nullable (string?)
            builder.Property(s => s.Name)
                .HasMaxLength(100);

            // Address pasó a ser Street en tu entidad
            builder.Property(s => s.Street)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            builder.Property(s => s.HasPlanogram)
                .IsRequired();

            // Relación con City
            builder.HasOne(s => s.City)
                .WithMany() // Asumiendo que City no tiene una colección explícita de Stores
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con District
            builder.HasOne(s => s.District)
                .WithMany() // Asumiendo que District no tiene una colección explícita de Stores
                .HasForeignKey(s => s.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}