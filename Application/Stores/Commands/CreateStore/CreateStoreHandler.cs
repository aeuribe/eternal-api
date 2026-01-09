using eternal_api.Domain.Entities;
using eternal_api.Application.Common.Interfaces;
using MediatR;

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
            var store = new Store(command.Name, command.Address, command.CityId);
            await _storeRepository.AddAsync(store);
            return store.Id;
        }
    }

}
