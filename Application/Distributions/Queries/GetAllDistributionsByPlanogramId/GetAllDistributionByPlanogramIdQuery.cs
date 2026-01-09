using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Distributions.Queries.GetAllDistributionsByPlanogramId
{
    public class GetAllDistributionByPlanogramIdQuery : IRequest<IEnumerable<DistributionDto>>
    {
        public Guid PlanogramId { set; get; }
    }
}
