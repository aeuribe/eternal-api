using MediatR;

namespace eternal_api.Application.Families.Commands.DesactivateCategory
{
    public class DesactivateActivateFamilyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
