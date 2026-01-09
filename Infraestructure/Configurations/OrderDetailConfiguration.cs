using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            // Nombre de la tabla
            builder.ToTable("ORDER_DETAIL");

            // Clave primaria
            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(e => e.Quantity)
                .HasColumnName("QUANTITY")
                .IsRequired();

            builder.Property(e => e.OrderId)
                .HasColumnName("ORDER_ID")
                .IsRequired();

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCT_ID")
                .IsRequired();

            // Relaciones
            builder.HasOne<Order>() // relación con Order
                .WithMany(o => o.orderDetails) // asumiendo que Order tiene ICollection<OrderDetail>
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>() // relación con Product
                .WithMany() // si Product no tiene colección de OrderDetails
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}