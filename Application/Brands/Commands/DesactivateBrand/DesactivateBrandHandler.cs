using eternal_api.Application.Brands.Interfaces;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eternal_api.Application.Brands.Commands.DesactivateBrand
{
    public class DesactivateBrandHandler : IRequestHandler<DesactivateBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;

        public DesactivateBrandHandler(IBrandRepository brandRepository) 
        {
            _brandRepository = brandRepository;
        }
        public async Task<bool> Handle(DesactivateBrandCommand command, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(command.Id);
            if (brand is null) return false;

            brand.ToogleStatus();

            await _brandRepository.UpdateAsync(brand);
            return true;
        }
    }
}
