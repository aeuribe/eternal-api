using eternal_api.Application.Brands.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Brands.Commands.CreateBrand
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        private readonly IBrandRepository _brandRepository;

        public CreateBrandHandler(IBrandRepository brandRepository) 
        {
            _brandRepository = brandRepository;
        }
        public async Task<Guid> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
        {
            var brand = new Brand(command.Name);

            await _brandRepository.AddAsyncBrand(brand);
            return brand.Id;
        }
    }
}
