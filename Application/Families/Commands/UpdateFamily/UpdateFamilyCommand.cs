using MediatR;

namespace eternal_api.Application.Families.Commands.UpdateCategory
{
    public class UpdateFamilyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FamilyCode { get; set; }
        public Guid BrandId { get; set; }
        public Guid ClassId { get; set; }
    }
}
