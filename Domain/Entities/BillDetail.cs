namespace eternal_api.Domain.Entities
{
    public class BillDetail
    {
        // ID único de BillDetail
        public int Id { get; set; }

        // Cantidad facturada por producto
        public int Quantity { get; set; }

        // Subtotal facturado por producto
        public float SubTotal { get; set; }
        
        // Clave foránea de producto
        public int ProductId { get; set; }

        // Clave foránea de Bill
        public int BillId { get; set; }
    }
}
