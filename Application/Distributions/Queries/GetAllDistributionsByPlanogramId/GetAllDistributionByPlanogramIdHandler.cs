using eternal_api.Application.Distributions.Interfaces;
using eternal_api.Application.Distributions.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Distributions.Queries.GetAllDistributionsByPlanogramId
{
    public class GetAllDistributionByPlanogramIdHandler : IRequestHandler<GetAllDistributionByPlanogramIdQuery, IEnumerable<DistributionDto>>
    {
        private readonly IDistributionRepository _distributionRepository;

        public GetAllDistributionByPlanogramIdHandler(IDistributionRepository distributionRepository)
        {
            _distributionRepository = distributionRepository;
        }

        public async Task<IEnumerable<DistributionDto>> Handle(GetAllDistributionByPlanogramIdQuery command, CancellationToken cancellationToken)
        {
            var distributions = await _distributionRepository.ListAsync(command.PlanogramId);

            return distributions.Select(d => new DistributionDto 
            { 
                Id = d.Id,
                ProductId = d.ProductId,
                PlanogramId = d.PlanogramId,
                Xposition = d.Xposition,
                Yposition = d.Yposition
            }).ToList();
        }
    }
}
