using MediatR;

namespace eternal_api.Application.Distributions.Commands.AddDistribution
{
    public class AddDistributionCommand : IRequest<Guid>
    {
        public Guid ProductId { set; get; }
        public Guid PlanogramId { set; get; }
        public int Xposition { set; get; }
        public int Yposition { set; get; }
    }
}
