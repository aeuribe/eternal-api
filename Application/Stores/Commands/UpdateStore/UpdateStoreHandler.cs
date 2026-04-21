using eternal_api.Application.Stores.Interfaces;
using MediatR;

namespace eternal_api.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreHandler : IRequestHandler<UpdateStoreCommand, bool>
    {
        private readonly IStoreRepository _storeRepository;

        public UpdateStoreHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<bool> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(command.Id);

            if (store is null) return false;

            // Llamamos al método Update de la entidad con los nuevos parámetros
            store.Update(
                command.StoreNumber,
                command.ZoneNumber,
                command.ZipCode,
                command.Name,
                command.Street,
                command.HasPlanogram,
                command.CityId,
                command.DistrictId
            );

            await _storeRepository.UpdateAsync(store);
            return true;
        }
    }
}