using MediatR;

namespace eternal_api.Application.Presentations.Commands.UpdatePresentation
{
    public class UpdatePresentationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string GenericCode { get; set; } = string.Empty;
        public decimal? Volume { get; set; }
        public string? Unit { get; set; }
        public Guid FamilyId { get; set; }
    }
}
