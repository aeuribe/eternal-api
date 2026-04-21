using MediatR;

namespace eternal_api.Application.Presentations.Commands.CreatePresentation
{
    public class CreatePresentationCommand : IRequest<Guid>
    {
        public string GenericCode { get; set; } = string.Empty;
        public decimal? Volume { get; set; }
        public string? Unit { get; set; }
        public Guid FamilyId { get; set; }
        
    }


}
