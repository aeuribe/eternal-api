using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Planograms.Queries.GetAllPlanograms
{
    public class GetAllPlanogramsQuery : IRequest<IEnumerable<PlanogramDto>>
    {
    }
}
