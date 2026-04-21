using MediatR;

namespace eternal_api.Application.Planograms.Commands.CreatePlanogram
{
    public class CreatePlanogramCommand : IRequest<Guid> 
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
