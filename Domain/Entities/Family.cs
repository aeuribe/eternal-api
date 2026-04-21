namespace eternal_api.Domain.Entities
{
    public class Family
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string FamilyCode { get; set; }
        public bool isActive { get; set; } = true;

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public Family() { }
        public Family(string name, string familyCode, Guid brandId, Guid classId) 
        {
            Name = name;
            FamilyCode = familyCode;
            BrandId = brandId;
            ClassId = classId;
        }

        public void Update(string name, string familyCode, Guid brandId, Guid classId)
        {
            Name = name;
            FamilyCode = familyCode;
            BrandId = brandId;
            ClassId = classId;
        }

        public void ToogleStatus() 
        {
            isActive = !isActive;
        }
    }
}
