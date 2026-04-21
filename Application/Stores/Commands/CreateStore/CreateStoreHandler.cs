using eternal_api.Domain.Entities;
using MediatR;
using eternal_api.Application.Stores.Interfaces;

namespace eternal_api.Application.Stores.Commands.CreateStore
{
    public class CreateStoreHandler : IRequestHandler<CreateStoreCommand, Guid>
    {
        private readonly IStoreRepository _storeRepository;

        public CreateStoreHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<Guid> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
        {
            // Instanciamos usando el nuevo constructor de la entidad
            var store = new Store(
                command.StoreNumber,
                command.ZoneNumber,
                command.ZipCode,
                command.Name,
                command.Street,
                command.HasPlanogram,
                command.CityId,
                command.DistrictId
            );

            await _storeRepository.AddAsync(store);
            return store.Id;
        }
    }
}