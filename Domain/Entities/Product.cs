namespace eternal_api.Domain.Entities
{
    public class Product
    {
        // ID único del producto
        public int Id { get; set; }

        // Nombre del producto
        public string Name { get; set; }

        // Categoria del producto
        public string Category { get; set; }

        // SKU del producto
        public string SKU { get; set; }

        // Status de disponibilidad del producto
        public bool isActive { get; set; }

    }
}
