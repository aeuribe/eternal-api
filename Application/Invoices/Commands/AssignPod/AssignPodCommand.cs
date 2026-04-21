using MediatR;

namespace eternal_api.Application.Invoices.Commands.AssignPOD
{
    public class AssignPODCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string POD { get; set; }
    }
}
