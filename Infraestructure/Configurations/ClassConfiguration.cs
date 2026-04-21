using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("CLASS");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.IsActive)
                .HasColumnName("IS_ACTIVE")
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
