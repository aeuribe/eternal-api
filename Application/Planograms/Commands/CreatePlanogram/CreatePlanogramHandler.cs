using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;
using System.Reflection.Metadata;

namespace eternal_api.Application.Planograms.Commands.CreatePlanogram
{
    public class CreatePlanogramHandler : IRequestHandler<CreatePlanogramCommand, Guid>
    {
        public readonly IPlanogramRepository _planogramRepository;

        public CreatePlanogramHandler(IPlanogramRepository planogramRepository)
        {
            _planogramRepository = planogramRepository;
        }

        public async Task<Guid> Handle(CreatePlanogramCommand command, CancellationToken cancellationToken)
        {
            var planogram = new Planogram();

            if (planogram == null) return Guid.Empty;

            await _planogramRepository.AddAsync(planogram);

            return planogram.Id;
        }
    }
}
