using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Planograms.Commands.DeletePlanogram;
using MediatR;

namespace eternal_api.Application.Planograms.Queries.GetPlanogramById
{
    public class GetPlanogramByIdHandler : IRequestHandler<GetPlanogramByIdQuery, PlanogramDto>
    {
        public readonly IPlanogramRepository _planogramRepository;

        public GetPlanogramByIdHandler(IPlanogramRepository planogramRepository)
        {
            _planogramRepository = planogramRepository;
        }

        public async Task<PlanogramDto> Handle(GetPlanogramByIdQuery command, CancellationToken cancellationToken)
        {
            var planogram = await _planogramRepository.GetPlanogramById(command.Id);
            if (planogram == null) return null;

            return new PlanogramDto
            {
                Id = planogram.Id,
                CreatedAt = planogram.CreatedAt,
                isActive = planogram.isActive
            };

        }
    }
}
