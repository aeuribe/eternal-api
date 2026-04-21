using eternal_api.Application.Families.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Families.Queries.GetAllCategories
{
    public class GetAllFamiliesQuery : IRequest<IEnumerable<FamilyDto>>
    {
    }
}
