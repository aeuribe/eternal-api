using MediatR;

namespace eternal_api.Application.Families.Commands.DeleteFamily
{
    public class DeleteFamilyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
