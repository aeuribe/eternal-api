namespace eternal_api.Domain.Entities
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public Class() { }
        public Class(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsActive = true;
        }

        public void ToggleStatus()
        {
            IsActive = !IsActive;
        }
    }
}
