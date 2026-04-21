using eternal_api.Application.Presentations.Interfaces;
using MediatR;

namespace eternal_api.Application.Presentations.Commands.ToggleStatus
{
    public class ToggleStatusPresentationHandler : IRequestHandler<ToggleStatusPresentationCommand, bool>
    {
        private readonly IPresentationRepository _repository;

        public ToggleStatusPresentationHandler(IPresentationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ToggleStatusPresentationCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar la presentación
            var presentation = await _repository.GetByIdAsync(request.Id);

            if (presentation == null)
            {
                return false; // No se encontró la entidad
            }

            // 2. Cambiar el estado usando el método de dominio
            presentation.ToggleStatus();

            // 3. Actualizar en el repositorio
            await _repository.UpdateAsync(presentation);

            return true;
        }
    }
}