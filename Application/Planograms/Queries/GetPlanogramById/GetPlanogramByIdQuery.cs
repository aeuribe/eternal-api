using eternal_api.Application.Planograms.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Planograms.Queries.GetPlanogramById
{
    public class GetPlanogramByIdQuery : IRequest<PlanogramDto>
    {
        public Guid Id { set; get; }
    }
}
