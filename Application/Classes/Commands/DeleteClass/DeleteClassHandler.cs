using eternal_api.Application.Classes.Interfaces;
using MediatR;

namespace eternal_api.Application.Classes.Commands.DeleteClass
{
    public class DeleteClassHandler : IRequestHandler<DeleteClassCommand, bool>
    {
        private readonly IClassRepository _classRepository;
        private readonly IClassFamilyValidationService _classFamilyValidationService;

        public DeleteClassHandler(IClassRepository classRepository, IClassFamilyValidationService classFamilyValidationService)
        {
            _classRepository = classRepository;
            _classFamilyValidationService = classFamilyValidationService;
        }

        public async Task<bool> Handle(DeleteClassCommand command, CancellationToken cancellationToken)
        {
            var @class = await _classRepository.GetByIdAsync(command.Id);
            if (@class is null) return false;

            bool hasFamilies = await _classFamilyValidationService.HasAnyFamilyAssociatedAsync(command.Id);
            if (hasFamilies)
            {
                throw new InvalidOperationException("No puedes eliminar esta clase porque tiene familias asociadas. Desactívala si necesitas ocultarla.");
            }

            return await _classRepository.DeleteAsync(@class);
        }
    }
}
