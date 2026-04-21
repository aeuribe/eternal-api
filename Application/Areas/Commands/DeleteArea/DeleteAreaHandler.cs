using MediatR;

namespace eternal_api.Application.Areas.Commands.DeleteArea
{
    public class DeleteAreaHandler : IRequestHandler<DeleteAreaCommand, (bool Succeeded, string ErrorMessage)>
    {
        private readonly IAreaRepository _repository;
        public DeleteAreaHandler(IAreaRepository repository) => _repository = repository;

        public async Task<(bool Succeeded, string ErrorMessage)> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(request.Id);
            if (area == null) return (false, "Área no encontrada.");

            // Regla de Negocio: No borrar si tiene hijos
            var hasRegions = await _repository.HasRegionsAsync(request.Id);
            if (hasRegions)
            {
                return (false, "No se puede eliminar el área porque tiene regiones asociadas.");
            }

            await _repository.DeleteAsync(area);
            return (true, string.Empty);
        }
    }
}