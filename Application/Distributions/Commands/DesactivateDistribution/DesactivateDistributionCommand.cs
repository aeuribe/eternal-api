using MediatR;

namespace eternal_api.Application.Distributions.Commands.DesactivateDistribution
{
    public class DesactivateDistributionCommand : IRequest<bool>
    { 
        public Guid Id { set; get; }
    }
}
