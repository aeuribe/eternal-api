using eternal_api.Application.Brands.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eternal_api.Application.Brands.Commands.UpdateBrand
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;

        public UpdateBrandHandler(IBrandRepository brandRepository) 
        {
            _brandRepository = brandRepository;
        }
        public async Task<bool> Handle(UpdateBrandCommand command, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(command.Id);
            if (brand is null) return false;

            brand.Update(command.Name);

            await _brandRepository.UpdateAsync(brand);
            return true;
        }
    }
}
