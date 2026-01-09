using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Distributions.Commands.DeleteDistribution
{
    public class DeleteDistributionHandler : IRequestHandler<DeleteDistributionCommand, bool>
    {
        private readonly IDistributionRepository _distributionRepository;

        public DeleteDistributionHandler(IDistributionRepository distributionRepository)
        {
            _distributionRepository = distributionRepository;
        }

        public async Task<bool> Handle(DeleteDistributionCommand command, CancellationToken cancellationToken)
        {
            var distribution = await _distributionRepository.GetByIdAsync(command.Id);
            if (distribution == null) return false;

            await _distributionRepository.DeleteAsync(distribution);
            return true;
        }
    }
}
