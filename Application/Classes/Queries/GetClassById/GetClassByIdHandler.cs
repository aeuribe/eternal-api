using eternal_api.Application.Classes.Interfaces;
using eternal_api.Application.Classes.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Classes.Queries.GetClassById
{
    public class GetClassByIdHandler : IRequestHandler<GetClassByIdQuery, ClassDto?>
    {
        private readonly IClassRepository _classRepository;

        public GetClassByIdHandler(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<ClassDto?> Handle(GetClassByIdQuery query, CancellationToken cancellationToken)
        {
            var @class = await _classRepository.GetByIdAsync(query.Id);
            if (@class is null) return null;

            return new ClassDto
            {
                Id = @class.Id,
                Name = @class.Name,
                IsActive = @class.IsActive
            };
        }
    }
}
