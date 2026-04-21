using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class DistributionConfiguration : IEntityTypeConfiguration<Distribution>
    {
        public void Configure(EntityTypeBuilder<Distribution> builder)
        {
            builder.ToTable("DISTRIBUTION");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(e => e.Xposition)
                .HasColumnName("X_POSITION")
                .IsRequired();

            builder.Property(e => e.Yposition)
                .HasColumnName("Y_POSITION")
                .IsRequired();

            builder.Property(e => e.PlanogramId)
                .HasColumnName("PLANOGRAM_ID")
                .IsRequired();

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCT_ID")
                .IsRequired();

            // Relaciones
            builder.HasOne<Planogram>() // suponiendo que existe la entidad Planogram
                .WithMany()
                .HasForeignKey(e => e.PlanogramId)
                .OnDelete(DeleteBehavior.Restrict);

            // 👇 LA CORRECCIÓN VA AQUÍ 👇
            builder.HasOne(e => e.Product) // Enlazamos explícitamente la propiedad de la clase
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}