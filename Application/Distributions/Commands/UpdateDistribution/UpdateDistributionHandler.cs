using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eternal_api.Application.Distributions.Commands.UpdateDistribution
{
    public class UpdateDistributionHandler : IRequestHandler<UpdateDistributionCommand, bool>
    {
        private readonly IDistributionRepository _distributionRepository;

        public UpdateDistributionHandler(IDistributionRepository distributionRepository)
        {
            _distributionRepository = distributionRepository;
        }

        public async Task<bool> Handle(UpdateDistributionCommand command, CancellationToken cancellationToken)
        {
            var distribution = await _distributionRepository.GetByIdAsync(command.Id);
            if (distribution == null) return false;

            distribution.Update(command.ProductId, command.ProductId, command.Xposition, command.Yposition);
            await _distributionRepository.UpdateAsync(distribution);
            return true;
        }
    }
}