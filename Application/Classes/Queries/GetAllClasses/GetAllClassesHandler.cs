using eternal_api.Application.Classes.Interfaces;
using eternal_api.Application.Classes.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Classes.Queries.GetAllClasses
{
    public class GetAllClassesHandler : IRequestHandler<GetAllClassesQuery, IEnumerable<ClassDto>>
    {
        private readonly IClassRepository _classRepository;

        public GetAllClassesHandler(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<ClassDto>> Handle(GetAllClassesQuery query, CancellationToken cancellationToken)
        {
            var classes = await _classRepository.ListAsync();

            return classes.Select(c => new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive
            }).ToList();
        }
    }
}
