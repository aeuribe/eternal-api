using eternal_api.Application.Planograms.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Planograms.Commands.DesactivatePlanogram
{
    public class DesactivatePlanogramHandler : IRequestHandler<DesactivatePlanogramCommand, bool>
    {
        public readonly IPlanogramRepository _planogramRepository;

        public DesactivatePlanogramHandler(IPlanogramRepository planogramRepository)
        {
            _planogramRepository = planogramRepository;
        }

        public async Task<bool> Handle(DesactivatePlanogramCommand command, CancellationToken cancellationToken)
        {
            var planogram = await _planogramRepository.GetPlanogramById(command.Id);

            if (planogram is null) return false;

            planogram.Desactivate();

            await _planogramRepository.UpdateAsync(planogram);

            return true;


        }
    }
}
