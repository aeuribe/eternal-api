using MediatR;

namespace eternal_api.Application.Distributions.Commands.UpdateDistribution
{
    public class UpdateDistributionCommand : IRequest<bool>
    {
        public Guid Id { set; get; }
        public Guid ProductId { set; get; }
        public Guid PlanogramId { set; get; }
        public int Xposition { set; get; }
        public int Yposition { set; get; }
    }
}
