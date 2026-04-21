
namespace eternal_api.Domain.Entities
{
    public class Product
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public string ImageFileName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
        public string Sku { get; set; } 

        public Guid PresentationId { get; set; }
        public Presentation Presentation { get; set; }


        // Constructor para creación actualizado
        public Product(string name, string shortName, string code, Guid presentationId, string sku)
        {
            Id = Guid.NewGuid();
            Name = name;
            ShortName = shortName;
            Code = code;
            PresentationId = presentationId;
            Sku = sku;
        }

        // Método Update actualizado
        public void Update(string name, string shortName, string code, Guid presentationId, string sku)
        {
            Name = name;
            ShortName = shortName;
            Code = code;
            PresentationId = presentationId;
            Sku = sku;
        }

        public void UploadImage(string image)
        {
            ImageFileName = image;
        }

        public void Deactivate() => isActive = !isActive;
    }
}