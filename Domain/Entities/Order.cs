using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace eternal_api.Domain.Entities
{
    public class Order
    {
        // ID único de Order
        public Guid Id { get; set; }

        // Fecha de creación de la orden
        public DateTime CreatedAt { get; set; }

        // Número de PO
        public string? PO { get; set; }

        // Status de la orden que son "pending" y "invoiced"
        public string Status { get; set; }

        // Uso exclusivo de EF Core
        public Guid SalespersonId { get; set; }
        public User Salesperson { get; set; }

        // Uso exclusivo de EF Core
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<OrderDetail> orderDetails { set; get; }

        public Order() { }
        public Order(Guid salespersonId, Guid storeId, string po)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Status = "pending";
            PO = po;
            SalespersonId = salespersonId;
            StoreId = storeId;
        }

        public void Update(Guid salespersonId, Guid storeId, string PO)
        {
            SalespersonId = salespersonId;
            StoreId = storeId;
            this.PO = PO;
        }
        public void UpdateStatusToInvoiced() 
        {
            Status = "invoiced";
        }

    }
}
