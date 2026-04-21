using eternal_api.Application.Presentations.Interfaces;
using MediatR;

namespace eternal_api.Application.Presentations.Commands.DeletePresentation
{
    public class DeletePresentationHandler : IRequestHandler<DeletePresentationCommand, bool>
    {
        private readonly IPresentationRepository _presentationRepository;
        private readonly IPresentationProductValidationService _presentationProductValidationService;

        public DeletePresentationHandler(
            IPresentationRepository presentationRepository,
            IPresentationProductValidationService presentationProductValidationService)
        {
            _presentationRepository = presentationRepository;
            _presentationProductValidationService = presentationProductValidationService;
        }

        public async Task<bool> Handle(DeletePresentationCommand command, CancellationToken cancellationToken)
        {
            var presentation = await _presentationRepository.GetByIdAsync(command.Id);
            if (presentation is null) return false;

            var hasAnyProduct = await _presentationProductValidationService.HasAnyProductAssociatedAsync(command.Id);
            var hasAnyProductWithOrders = await _presentationProductValidationService.HasAnyProductWithOrdersAssociatedAsync(command.Id);

            if (hasAnyProduct && hasAnyProductWithOrders)
            {
                throw new InvalidOperationException("No se puede eliminar la presentación porque tiene productos con historial de órdenes.");
            }

            return await _presentationRepository.DeleteAsync(presentation);
        }
    }
}
