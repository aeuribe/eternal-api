using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Planograms.Commands.UpdatePlanogram;
using MediatR;

namespace eternal_api.Application.Planograms.Commands.DeletePlanogram
{
    public class DeletePlanogramHandler : IRequestHandler<DeletePlanogramCommand, bool>
    {
        public readonly IPlanogramRepository _planogramRepository;

        public DeletePlanogramHandler(IPlanogramRepository planogramRepository)
        {
            _planogramRepository = planogramRepository;
        }

        public async Task<bool> Handle(DeletePlanogramCommand command, CancellationToken cancellationToken)
        {
            var planogram = await _planogramRepository.GetPlanogramById(command.Id);
            if (planogram == null) return false;

            await _planogramRepository.DeleteAsync(planogram);

            return true;
        }
    }
}
