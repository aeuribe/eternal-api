using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            // Nombre de la tabla
            builder.ToTable("INVOICE_DETAIL");

            // Clave primaria
            builder.HasKey(e => e.Id);

            // Propiedades
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(e => e.Quantity)
                .HasColumnName("QUANTITY")
                .IsRequired();

            builder.Property(e => e.Subtotal)
                .HasColumnName("SUBTOTAL")
                .IsRequired();

            builder.Property(e => e.InvoiceId)
                .HasColumnName("INVOICE_ID")
                .IsRequired();

            builder.Property(e => e.ProductId)
                .HasColumnName("PRODUCT_ID")
                .IsRequired();

            // Relaciones
            builder.HasOne<Invoice>() // relación con Invoice
                .WithMany(i => i.invoiceDetails) // si tu Invoice tiene ICollection<InvoiceDetail> cámbialo aquí
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>() // relación con Product
                .WithMany() // si Product tiene ICollection<InvoiceDetail>, cámbialo por .WithMany(p => p.InvoiceDetails)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}