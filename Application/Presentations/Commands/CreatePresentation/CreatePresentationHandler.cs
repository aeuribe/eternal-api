using eternal_api.Application.Presentations.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Presentations.Commands.CreatePresentation
{
    public class CreatePresentationHandler : IRequestHandler<CreatePresentationCommand, Guid>
    {
        private readonly IPresentationRepository _presentationRepository;

        public CreatePresentationHandler(IPresentationRepository presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }

        public async Task<Guid> Handle(CreatePresentationCommand command, CancellationToken cancellationToken)
        {
            // 1. Validamos por GenericCode (UPC) en lugar de SKU
            var existing = await _presentationRepository.GetByGenericCodeAsync(command.GenericCode);
            if (existing is not null)
            {
                throw new InvalidOperationException($"Ya existe una presentación con el código genérico (UPC) '{command.GenericCode}'.");
            }

            // 2. Instanciamos sin el SKU
            var presentation = new Presentation(
                command.GenericCode,
                command.Volume,
                command.Unit,
                command.FamilyId
            );

            await _presentationRepository.AddAsync(presentation);

            return presentation.Id;
        }
    }
}