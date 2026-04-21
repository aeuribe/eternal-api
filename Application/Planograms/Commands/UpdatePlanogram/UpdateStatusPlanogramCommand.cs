using MediatR;

namespace eternal_api.Application.Planograms.Commands.UpdatePlanogram
{
    public class UpdateStatusPlanogramCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
