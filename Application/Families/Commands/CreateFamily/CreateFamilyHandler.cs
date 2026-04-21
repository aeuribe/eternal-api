using eternal_api.Application.Families.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Families.Commands.CreateFamily
{
    public class CreateFamilyHandler : IRequestHandler<CreateFamilyCommand, Guid>
    {
        public IFamilyRepository _familyRepository;

        public CreateFamilyHandler(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }
        public async Task<Guid> Handle(CreateFamilyCommand command, CancellationToken cancellationToken)
        {
            // 1. Intentamos buscar la familia por el código que viene en el comando
            // Asumo que tu repositorio tiene un método para buscar por Code
            var existingFamily = await _familyRepository.GetByCodeAsync(command.FamilyCode);

            if (existingFamily != null)
            {
                // Si no es nulo, significa que el código ya está ocupado
                throw new Exception($"Regla de Negocio: El código '{command.FamilyCode}' ya está asignado a la familia '{existingFamily.Name}'.");
            }

            // 2. Si llegamos aquí, el código está libre. Procedemos a crear.
            var family = new Family(
                command.Name,
                command.FamilyCode,
                command.BrandId,
                command.ClassId
            );

            await _familyRepository.AddAsyncFamily(family);

            // 3. Importante: Si el repositorio no hace el SaveChanges interno, 
            // recuerda que deberías usar el UnitOfWork aquí también si quieres transaccionalidad.

            return family.Id;
        }
    }
}
