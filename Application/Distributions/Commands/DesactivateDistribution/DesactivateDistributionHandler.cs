using eternal_api.Application.Distributions.Interfaces;
using MediatR;

namespace eternal_api.Application.Distributions.Commands.DesactivateDistribution
{
    public class DesactivateDistributionHandler : IRequestHandler<DesactivateDistributionCommand, bool>
    {
        private readonly IDistributionRepository _distributionRepository;

        public DesactivateDistributionHandler(IDistributionRepository distributionRepository)
        {
            _distributionRepository = distributionRepository;
        }

        public async Task<bool> Handle(DesactivateDistributionCommand command, CancellationToken cancellationToken)
        {
            var distribution = await _distributionRepository.GetByIdAsync(command.Id);
            if (distribution == null) return false;

            await _distributionRepository.DeleteAsync(distribution);
            return true;
        }
    }
}
