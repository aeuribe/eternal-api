using eternal_api.Application.Distributions.Interfaces;
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
            // 1. Buscamos la distribución
            var distribution = await _distributionRepository.GetByIdAsync(command.Id);
            if (distribution == null) return false;

            // 2. LA REGLA DE NEGOCIO DE TU TESIS
            // Revisamos si el planograma actual tiene órdenes asociadas
            bool hasOrders = await _distributionRepository.HasOrdersInPlanogramAsync(distribution.PlanogramId);

            if (hasOrders)
            {
                // Lanzamos la excepción que tu Middleware atrapará para mostrarle al usuario
                throw new Exception("No se puede editar esta distribución porque el planograma ya tiene órdenes asociadas. Debe dejarlo como está.");
            }

            // 3. Si no hay órdenes, procedemos a actualizar
            distribution.Update(command.ProductId, command.PlanogramId, command.Xposition, command.Yposition);
            await _distributionRepository.UpdateAsync(distribution);

            return true;
        }
    }
}