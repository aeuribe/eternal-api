using MediatR;

namespace eternal_api.Application.Planograms.Commands.DesactivatePlanogram
{
    public class DesactivatePlanogramCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
