using eternal_api.Application.Families.Interfaces;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eternal_api.Application.Families.Commands.DeleteFamily
{
    public class DeleteFamilyHandler : IRequestHandler<DeleteFamilyCommand, bool>
    {
        private readonly IFamilyRepository _familyRepository;
        public DeleteFamilyHandler(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }
        public async Task<bool> Handle(DeleteFamilyCommand command, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.GetByIdAsync(command.Id);
            if (family is null) return false;

            await _familyRepository.DeleteAsync(family);
            return true;
        }
    }
}
