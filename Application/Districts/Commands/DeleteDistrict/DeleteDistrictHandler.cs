using eternal_api.Application.Districts.Interfaces;
using MediatR;

namespace eternal_api.Application.Districts.Commands.DeleteDistrict
{
    public class DeleteDistrictHandler : IRequestHandler<DeleteDistrictCommand, (bool Succeeded, string ErrorMessage)>
    {
        private readonly IDistrictRepository _repository;
        public DeleteDistrictHandler(IDistrictRepository repository) => _repository = repository;

        public async Task<(bool Succeeded, string ErrorMessage)> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            var district = await _repository.GetByIdAsync(request.Id);
            if (district == null) return (false, "Distrito no encontrado.");

            // Regla de Negocio: No borrar si tiene Tiendas (Stores)
            var hasStores = await _repository.HasStoresAsync(request.Id);
            if (hasStores)
            {
                return (false, "No se puede eliminar el distrito porque tiene tiendas asociadas.");
            }

            await _repository.DeleteAsync(district);
            return (true, string.Empty);
        }
    }
}
