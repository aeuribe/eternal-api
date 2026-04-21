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
            builder.HasOne(d => d.Order)           // El hijo tiene UNA orden
                    .WithMany(o => o.orderDetails)    // El padre tiene MUCHOS detalles
                    .HasForeignKey(d => d.OrderId)    // La llave es OrderId (sin el 1)
                    .OnDelete(DeleteBehavior.Cascade);

            // Partimos del detalle (Muchos) hacia el Producto (Uno)
            builder.HasOne(d => d.Product)       // El detalle tiene UN Producto
                .WithMany()                      // El Producto tiene MUCHOS detalles (pero no los exponemos en la clase Product)
                .HasForeignKey(d => d.ProductId) // La llave foránea es ProductId
                .OnDelete(DeleteBehavior.Restrict); // No dejamos borrar productos con órdenes vivas
        }
    }
}