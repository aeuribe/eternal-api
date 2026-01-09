using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Distributions.Commands.AddDistribution
{
    public class AddDistributionHandler : IRequestHandler<AddDistributionCommand, Guid>
    {
        public readonly IDistributionRepository _distributionRepository;

        public AddDistributionHandler(IDistributionRepository distributionRepository) 
        {
            _distributionRepository = distributionRepository;
        }

        public async Task<Guid> Handle(AddDistributionCommand command, CancellationToken cancellationToken)
        {
            var distribution = new Distribution(command.ProductId, command.PlanogramId, command.Xposition, command.Yposition);
            await _distributionRepository.AddAsync(distribution);

            return distribution.Id;
        }
    }
}
