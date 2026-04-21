using eternal_api.Application.Planograms.Interfaces;
using eternal_api.Application.Planograms.Queries.DTOs;
using eternal_api.Application.Planograms.Queries.GetPlanogramById;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Planograms.Queries.GetAllPlanograms
{
    public class GetAllPlanogramsHandler : IRequestHandler<GetAllPlanogramsQuery, IEnumerable<PlanogramDto>>
    {
        public readonly IPlanogramRepository _planogramRepository;

        public GetAllPlanogramsHandler(IPlanogramRepository planogramRepository)
        {
            _planogramRepository = planogramRepository;
        }

        public async Task<IEnumerable<PlanogramDto>> Handle(GetAllPlanogramsQuery query, CancellationToken cancellationToken)
        {
            var planograms = await _planogramRepository.GetAllAsync();

            return planograms.Select(o => new PlanogramDto
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                CreatedAt = o.CreatedAt,
                isActive = o.isActive
            }).ToList();

        }
    }
}
