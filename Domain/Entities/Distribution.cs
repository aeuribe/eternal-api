namespace eternal_api.Domain.Entities
{
    public class Distribution
    {
        // ID único de la distribución
        public int Id { get; set; }

        // Posicion X de la distribución
        public int Xposition { get; set; }

        // Posicion Y de la distribución
        public int Yposition { get; set; }

        // Planograma al que pertenece la distribución
        public int PlanogramId { get; set; }

        // Referencia foránea al producto
        public int ProductId { get; set; }

    }
}
