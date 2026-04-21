using eternal_api.Application.Areas.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Areas.Queries.GetAreaById
{
    public class GetAreaByIdHandler : IRequestHandler<GetAreaByIdQuery, AreaDto?>
    {
        private readonly IAreaRepository _repository;
        public GetAreaByIdHandler(IAreaRepository repository) => _repository = repository;

        public async Task<AreaDto?> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(request.Id);

            if (area == null) return null;

            return new AreaDto 
            { 
                Id = area.Id, 
                Name = area.Name 
            };
        }
    }
}
