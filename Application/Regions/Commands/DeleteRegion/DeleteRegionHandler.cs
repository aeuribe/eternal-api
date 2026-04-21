using eternal_api.Application.Regions.Interfaces;
using MediatR;

namespace eternal_api.Application.Regions.Commands.DeleteRegion
{
    public class DeleteRegionHandler : IRequestHandler<DeleteRegionCommand, (bool Succeeded, string ErrorMessage)>
    {
        private readonly IRegionRepository _repository;
        public DeleteRegionHandler(IRegionRepository repository) => _repository = repository;

        public async Task<(bool Succeeded, string ErrorMessage)> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await _repository.GetByIdAsync(request.Id);
            if (region == null) return (false, "Región no encontrada.");

            // Regla de Negocio: No borrar si tiene Distritos
            var hasDistricts = await _repository.HasDistrictsAsync(request.Id);
            if (hasDistricts)
            {
                return (false, "No se puede eliminar la región porque tiene distritos asociados.");
            }

            await _repository.DeleteAsync(region);
            return (true, string.Empty);
        }
    }
}
