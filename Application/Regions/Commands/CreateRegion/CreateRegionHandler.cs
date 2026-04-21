using eternal_api.Application.Regions.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Regions.Commands.CreateRegion
{
    public class CreateRegionHandler : IRequestHandler<CreateRegionCommand, Guid>
    {
        private readonly IRegionRepository _repository;
        public CreateRegionHandler(IRegionRepository repository) => _repository = repository;

        public async Task<Guid> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var region = new Region(request.AreaId, request.Name);
            await _repository.AddAsync(region);
            return region.Id;
        }
    }
}
