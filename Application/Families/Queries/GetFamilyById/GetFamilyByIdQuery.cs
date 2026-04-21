using eternal_api.Application.Families.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Families.Queries.GetCategoryById
{
    public class GetFamilyByIdQuery : IRequest<FamilyDto>
    {
        public Guid Id { set; get; }
    }
}
