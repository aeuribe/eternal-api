using eternal_api.Application.Stores.Interfaces;
using MediatR;

namespace eternal_api.Application.Stores.Commands.DesactivateStore
{
    public class DesactivateStoreHandler : IRequestHandler<DesactivateStoreCommand, bool>
    {
        private readonly IStoreRepository _storeRepository;

        public DesactivateStoreHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<bool> Handle(DesactivateStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(command.Id);
            if (store is null) return false;

            store.Desactivate();
            await _storeRepository.UpdateAsync(store);
            return true;
        }
    }

}
