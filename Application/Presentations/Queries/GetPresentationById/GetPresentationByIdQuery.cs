using eternal_api.Application.Presentations.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Presentations.Queries.GetPresentationById
{
    public class GetPresentationByIdQuery : IRequest<PresentationDto?>
    {
        public Guid Id { get; set; }
    }
}
