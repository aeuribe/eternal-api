using eternal_api.Application.Presentations.Interfaces;
using MediatR;

namespace eternal_api.Application.Presentations.Commands.UpdatePresentation
{
    public class UpdatePresentationHandler : IRequestHandler<UpdatePresentationCommand, bool>
    {
        private readonly IPresentationRepository _presentationRepository;
        private readonly IPresentationProductValidationService _presentationProductValidationService;

        public UpdatePresentationHandler(
            IPresentationRepository presentationRepository,
            IPresentationProductValidationService presentationProductValidationService)
        {
            _presentationRepository = presentationRepository;
            _presentationProductValidationService = presentationProductValidationService;
        }

        public async Task<bool> Handle(UpdatePresentationCommand command, CancellationToken cancellationToken)
        {
            var presentation = await _presentationRepository.GetByIdAsync(command.Id);
            if (presentation is null) return false;

            var hasAnyProduct = await _presentationProductValidationService.HasAnyProductAssociatedAsync(command.Id);
            var hasAnyProductWithOrders = await _presentationProductValidationService.HasAnyProductWithOrdersAssociatedAsync(command.Id);

            if (hasAnyProduct && hasAnyProductWithOrders)
            {
                throw new InvalidOperationException("No se puede modificar la presentación porque tiene productos con historial de órdenes.");
            }

            presentation.Update(command.GenericCode, command.Volume, command.Unit, command.FamilyId);
            await _presentationRepository.UpdateAsync(presentation);
            return true;
        }
    }
}
