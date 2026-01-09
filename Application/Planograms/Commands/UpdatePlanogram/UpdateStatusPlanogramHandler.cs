using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Planograms.Commands.UpdatePlanogram
{
    public class UpdateStatusPlanogramHandler : IRequestHandler<UpdateStatusPlanogramCommand, bool>
    {
        public readonly IPlanogramRepository _planogramRepository;

        public UpdateStatusPlanogramHandler(IPlanogramRepository planogramRepository)
        {
            _planogramRepository = planogramRepository;
        }

        public async Task<bool> Handle(UpdateStatusPlanogramCommand command, CancellationToken cancellationToken)
        {
            var planogram = await _planogramRepository.GetPlanogramById(command.Id);
            if (planogram == null)
                return false;

            // Update planogram properties here using command data
            // e.g. planogram.Name = command.Name;

            planogram.SwitchStatus();
            await _planogramRepository.UpdateAsync(planogram);
            return true;
        }
    }
}
