using eternal_api.Application.Planograms.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Planograms.Queries.GetAllPlanograms
{
    public class GetAllPlanogramsQuery : IRequest<IEnumerable<PlanogramDto>>
    {
    }
}
