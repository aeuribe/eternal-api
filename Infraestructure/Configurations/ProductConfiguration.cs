using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCT");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Code)
                .HasColumnName("CODE")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ShortName)
                .HasColumnName("SHORT_NAME")
                .HasMaxLength(100);

            builder.Property(p => p.isActive)
                .HasColumnName("IS_ACTIVE")
                .HasDefaultValue(true);

            builder.Property(p => p.PresentationId)
                .HasColumnName("PRESENTATION_ID")
                .IsRequired();

            builder.Property(p => p.ImageFileName)
                .HasColumnName("IMAGE_FILE_NAME")
                .IsRequired(false);

            builder.Property(p => p.Sku)
                .HasColumnName("SKU")
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.Presentation)
                .WithMany()
                .HasForeignKey(p => p.PresentationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}