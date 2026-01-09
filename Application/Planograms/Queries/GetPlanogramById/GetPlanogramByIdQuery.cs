using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Planograms.Queries.GetPlanogramById
{
    public class GetPlanogramByIdQuery : IRequest<PlanogramDto>
    {
        public Guid Id { set; get; }
    }
}
