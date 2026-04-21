using eternal_api.Application.Regions.Interfaces;
using MediatR;

namespace eternal_api.Application.Regions.Commands.UpdateRegion
{
    public class UpdateRegionHandler : IRequestHandler<UpdateRegionCommand, bool>
    {
        private readonly IRegionRepository _repository;
        public UpdateRegionHandler(IRegionRepository repository) => _repository = repository;

        public async Task<bool> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await _repository.GetByIdAsync(request.Id);
            if (region == null) return false;

            region.Update(request.AreaId, request.Name);
            await _repository.UpdateAsync(region);
            return true;
        }
    }
}
