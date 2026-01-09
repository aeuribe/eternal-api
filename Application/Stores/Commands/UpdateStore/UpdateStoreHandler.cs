
using eternal_api.Application.Common.Interfaces;
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

            store.Update(command.Name, command.Address, command.CityId);

            await _storeRepository.UpdateAsync(store);
            return true;
        }
    }

}
