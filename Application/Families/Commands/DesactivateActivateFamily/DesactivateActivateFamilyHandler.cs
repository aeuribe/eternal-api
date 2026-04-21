using eternal_api.Application.Families.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Families.Commands.DesactivateCategory
{
    public class DesactivateActivateFamilyHandler : IRequestHandler<DesactivateActivateFamilyCommand, bool>
    {
        public IFamilyRepository _familyRepository;

        public DesactivateActivateFamilyHandler(IFamilyRepository familyRepository) 
        {
            _familyRepository = familyRepository;
        }
        public async Task<bool> Handle(DesactivateActivateFamilyCommand command, CancellationToken cancellationToken)
        {
            var category = await _familyRepository.GetByIdAsync(command.Id);
            if (category is null) return false;

            category.ToogleStatus();

            await _familyRepository.UpdateAsync(category);
            return true;
        }
    }
}
