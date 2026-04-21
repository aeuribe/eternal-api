namespace eternal_api.Domain.Entities
{
    public class Presentation
    {
        public Guid Id { get; set; }
        public string GenericCode { get; set; }
        public decimal? Volume { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; } = true;

        public Guid FamilyId { get; set; }
        public Family Family { get; set; }

        public Presentation() { }
        public Presentation( string genericCode, decimal? volume, string? unit, Guid familyId)
        {
            GenericCode = genericCode;
            Volume = volume;
            Unit = unit;
            FamilyId = familyId;
        }

        public void Update(string genericCode, decimal? volume, string? unit, Guid familyId)
        {
            GenericCode = genericCode;
            Volume = volume;
            Unit = unit;
            FamilyId = familyId;
        }

        public void ToggleStatus()
        {
            IsActive = !IsActive;
        }
    }
}
