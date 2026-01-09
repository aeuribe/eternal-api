namespace eternal_api.Domain.Entities
{
    public class Product
    {
        // ID único del producto
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nombre del producto
        public string Name { get; set; } = string.Empty;

        // Categoria del producto
        public string Category { get; set; } = string.Empty;

        // SKU del producto
        public string SKU { get; set; } = string.Empty;

        // Status de disponibilidad del producto
        public bool isActive { get; set; } = true;

        // Constructor para creación
        public Product(string name, string category, string SKU)
        {
            Id = Guid.NewGuid();
            Name = name;
            Category = category;
            this.SKU = SKU;
        }

        public void Update(string name, string category, string sku)
        {
            Name = name;
            Category = category;
            SKU = sku;
        }

        // Método de dominio (ejemplo de lógica interna)
        public void Deactivate() => isActive = false;

        public void Activate() => isActive = true;

    }
}
