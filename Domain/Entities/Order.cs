using eternal_api.Domain.Enums;

namespace eternal_api.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PO { get; private set; } // private set para protegerlo

        public OrderStatus Status { get; private set; }

        public Guid SalespersonId { get; set; }
        public User Salesperson { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public Guid PlanogramId { get; set; }
        public Planogram? Planogram { get; set; }

        public Guid SalesRouteId { get; set; }
        public SalesRoute SalesRoute { get; set; }

        // 1. CORRECCIÓN CRÍTICA: Inicializamos la lista y usamos PascalCase
        public ICollection<OrderDetail> orderDetails { get; private set; } = new List<OrderDetail>();

        public Order() { }

        public Order(Guid id, Guid salespersonId, Guid storeId, Guid planogramId, string? po, Guid salesRouteId)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Created;
            SalespersonId = salespersonId;
            SalesRouteId = salesRouteId;
            StoreId = storeId;
            PlanogramId = planogramId;
            PO = po;
            
        }

        public void UpdateStore(Guid storeId)
        {
            StoreId = storeId;
        }

        // 2. NUEVO MÉTODO: Para que el Handler pueda actualizar el PO
        public void UpdatePo(string? po)
        {
            PO = po;
        }

        public void MarkAsInvoiced()
        {
            if (Status == OrderStatus.Canceled)
                throw new Exception("No se puede facturar una orden que ha sido cancelada.");

            Status = OrderStatus.Invoiced;
        }

        public void MarkAsCanceled()
        {
            if (Status == OrderStatus.Invoiced)
                throw new Exception("No se puede cancelar una orden que ya fue facturada. Debes emitir una nota de crédito.");

            Status = OrderStatus.Canceled;
        }

        public void UpdateDetails(IEnumerable<(Guid Id, Guid ProductId, int Quantity)> incomingItems)
        {
            // Extraemos los IDs de los detalles que vienen en el request
            var incomingDetailIds = incomingItems.Select(i => i.Id).ToList();

            // 1. ELIMINAR: Borramos los detalles cuyo ID ya no viene en la lista del frontend
            var itemsToRemove = orderDetails.Where(od => !incomingDetailIds.Contains(od.Id)).ToList();
            foreach (var item in itemsToRemove)
            {
                orderDetails.Remove(item);
            }

            // 2. ACTUALIZAR o AGREGAR
            foreach (var incoming in incomingItems)
            {
                // Buscamos por el ID exacto del detalle, no por el producto
                var existingDetail = orderDetails.FirstOrDefault(od => od.Id == incoming.Id);

                if (existingDetail != null)
                {
                    // El detalle existe (se creó antes o se sincronizó antes), lo actualizamos
                    existingDetail.Update(incoming.Quantity, incoming.ProductId);
                }
                else
                {
                    // Es un detalle totalmente nuevo creado offline, lo agregamos respetando su ID
                    orderDetails.Add(new OrderDetail(incoming.Id, incoming.Quantity, this.Id, incoming.ProductId));
                }
            }
        }

        public void AddDetail(Guid detailId, Guid productId, int quantity)
        {
            // Usamos el constructor de OrderDetail que ya configuramos antes
            orderDetails.Add(new OrderDetail(detailId, quantity, this.Id, productId ));
        }
    }
}