using eternal_api.Application.Families.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Families.Commands.UpdateCategory
{
    public class UpdateFamilyHandler : IRequestHandler<UpdateFamilyCommand, bool>
    {
        public IFamilyRepository _familyRepository;
        public UpdateFamilyHandler(IFamilyRepository familyRepository) 
        {
            _familyRepository = familyRepository;
        }
        public async Task<bool> Handle(UpdateFamilyCommand command, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.GetByIdAsync(command.Id);
            if (family is null) return false;

            family.Update(command.Name, command.FamilyCode, command.BrandId, command.ClassId);

            await _familyRepository.UpdateAsync(family);
            return true;
        }
    }
}
