using eternal_api.Domain.Entities;
using eternal_api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eternal_api.Infraestructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("ORDER");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CREATED_AT")
                .IsRequired();

            builder.Property(e => e.PO)
                .HasColumnName("PO")
                .HasMaxLength(50);

            builder.Property(e => e.Status)
                .HasColumnName("STATUS")
                .IsRequired()
                .HasDefaultValue(OrderStatus.Created);

            builder.Property(e => e.SalespersonId)
                .HasColumnName("SALESPERSON_ID")
                .IsRequired();

            builder.Property(e => e.StoreId)
                .HasColumnName("STORE_ID")
                .IsRequired();


            builder.Property(e => e.PlanogramId)
                .HasColumnName("PLANOGRAM_ID")
                .IsRequired();

            builder.Property(e => e.SalesRouteId)
                .HasColumnName("SALES_ROUTE_ID")
                .IsRequired();

            builder.HasOne(e => e.Salesperson)
                .WithMany()
                .HasForeignKey(e => e.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Store)
                .WithMany()
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SalesRoute)
                .WithMany()
                .HasForeignKey(e => e.SalesRouteId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}