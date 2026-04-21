using eternal_api.Application.Classes.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Classes.Queries.GetClassById
{
    public class GetClassByIdQuery : IRequest<ClassDto?>
    {
        public Guid Id { get; set; }
    }
}
