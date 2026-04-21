using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("CITY");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configuramos el Enum para que se guarde como string (ej. "FL", "TX")
            builder.Property(c => c.State)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(2); // Sabemos que los prefijos de estado de EE. UU. son siempre de 2 letras
        }
    }
}