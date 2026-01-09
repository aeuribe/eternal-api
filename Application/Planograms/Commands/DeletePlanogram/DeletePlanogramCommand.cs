using MediatR;

namespace eternal_api.Application.Planograms.Commands.DeletePlanogram
{
    public class DeletePlanogramCommand : IRequest<bool>
    {
        public Guid Id { set; get; }
    }
}
