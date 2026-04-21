using eternal_api.Application.Districts.Interfaces;
using MediatR;

namespace eternal_api.Application.Districts.Commands.UpdateDistrict
{
    public class UpdateDistrictHandler : IRequestHandler<UpdateDistrictCommand, bool>
    {
        private readonly IDistrictRepository _repository;
        public UpdateDistrictHandler(IDistrictRepository repository) => _repository = repository;

        public async Task<bool> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            var district = await _repository.GetByIdAsync(request.Id);
            if (district == null) return false;

            district.Update(request.RegionId, request.Name);
            await _repository.UpdateAsync(district);
            return true;
        }
    }
}
