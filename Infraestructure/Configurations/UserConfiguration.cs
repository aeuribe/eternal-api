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

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Rol)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Phone)
                .HasMaxLength(20);

            builder.Property(u => u.CityId)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);
        }
    }
}
