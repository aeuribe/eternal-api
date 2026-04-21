using eternal_api.Application.Areas.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Areas.Queries.GetAreaById
{
    public class GetAreaByIdQuery : IRequest<AreaDto?>
    {
        public Guid Id { get; set; }
    }
}
