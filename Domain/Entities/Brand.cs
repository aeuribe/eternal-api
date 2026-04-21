namespace eternal_api.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool isActive { get; set; } = true;

        public Brand(string name)
        {
            Name = name;
        }

        public void Update(string name) 
        {
            Name = name;
        }

        public void ToogleStatus() 
        {
            isActive = !isActive;
        }
    }
}
