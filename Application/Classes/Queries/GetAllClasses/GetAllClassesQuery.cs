using eternal_api.Application.Classes.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Classes.Queries.GetAllClasses
{
    public class GetAllClassesQuery : IRequest<IEnumerable<ClassDto>>
    {
    }
}
