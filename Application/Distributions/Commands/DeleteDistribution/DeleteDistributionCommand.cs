using MediatR;

namespace eternal_api.Application.Distributions.Commands.DeleteDistribution
{
    public class DeleteDistributionCommand : IRequest<bool>
    { 
        public Guid Id { set; get; }
    }
}
