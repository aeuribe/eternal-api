namespace eternal_api.Domain.Entities
{
    public class Distribution
    {
        // ID único de la distribución
        public Guid Id { get; set; } = Guid.NewGuid();

        // Posicion X de la distribución
        public int Xposition { get; set; }

        // Posicion Y de la distribución
        public int Yposition { get; set; }

        // Planograma al que pertenece la distribución
        public Guid PlanogramId { get; set; }

        // Referencia foránea al producto
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Distribution(Guid productId, Guid planogramId, int Xposition, int Yposition)
        {
            ProductId = productId;
            PlanogramId = planogramId;
            this.Xposition = Xposition;
            this.Yposition = Yposition;
        }

        public void Update(Guid productId, Guid planogramId, int Xpos, int Ypos)
        {
            ProductId = productId;
            PlanogramId = planogramId;
            Xposition = Xpos;
            Yposition = Ypos;
        }
    }

    
}
