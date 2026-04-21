using eternal_api.Application.Presentations.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Presentations.Queries.GetAllPresentations
{
    public class GetAllPresentationsQuery : IRequest<IEnumerable<PresentationDto>>
    {
    }
}
