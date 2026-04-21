using eternal_api.Application.Areas.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Areas.Queries.GetAreas
{
    public class GetAreasQuery: IRequest<IEnumerable<AreaDto>>{}
}
