using eternal_api.Application.Areas.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Areas.Queries.GetAreas
{
    public class GetAreasHandler : IRequestHandler<GetAreasQuery, IEnumerable<AreaDto>>
    {
        private readonly IAreaRepository _repository;
        public GetAreasHandler(IAreaRepository repository) => _repository = repository;

        public async Task<IEnumerable<AreaDto>> Handle(GetAreasQuery request, CancellationToken cancellationToken)
        {
            var areas = await _repository.GetAllAsync();

            // Usamos el inicializador de propiedades
            return areas.Select(a => new AreaDto
            {
                Id = a.Id,
                Name = a.Name
            });
        }
    }
}
