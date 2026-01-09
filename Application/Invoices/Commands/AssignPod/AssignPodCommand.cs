using MediatR;
namespace eternal_api.Application.Bills.Commands.AssignPod
{
    public class AssignPodCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
    }
}
