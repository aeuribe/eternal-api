using eternal_api.Application.Districts.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Districts.Commands.CreateDistrict
{
    public class CreateDistrictHandler : IRequestHandler<CreateDistrictCommand, Guid>
    {
        private readonly IDistrictRepository _repository;
        public CreateDistrictHandler(IDistrictRepository repository) => _repository = repository;

        public async Task<Guid> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            var district = new District(request.RegionId, request.Name);
            await _repository.AddAsync(district);
            return district.Id;
        }
    }
}
