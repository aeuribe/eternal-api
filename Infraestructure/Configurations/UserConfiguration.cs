using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Rol).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Phone).HasMaxLength(20);
            builder.Property(u => u.IsActive).HasDefaultValue(true);
            builder.Property(e => e.IdentityUserId).HasMaxLength(450).IsRequired();

            builder.HasIndex(e => e.IdentityUserId).IsUnique();

            // ==========================================
            // LA NUEVA RELACIÓN OPCIONAL CON LA RUTA
            // ==========================================
            builder.HasOne(u => u.SalesRoute)
                   .WithMany()
                   .HasForeignKey(u => u.SalesRouteId)
                   .IsRequired(false) // VITAL: Permite que el Guid sea null en la base de datos
                   .OnDelete(DeleteBehavior.SetNull); // Si por algún milagro se borra la ruta, el usuario queda "sin ruta" en lugar de ser eliminado
        }
    }
}