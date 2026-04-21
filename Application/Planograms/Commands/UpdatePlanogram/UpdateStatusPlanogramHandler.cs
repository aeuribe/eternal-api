using eternal_api.Application.Planograms.Interfaces;
using MediatR;

namespace eternal_api.Application.Planograms.Commands.UpdatePlanogram
{
    public class UpdateStatusPlanogramHandler : IRequestHandler<UpdateStatusPlanogramCommand, bool>
    {
        private readonly IPlanogramRepository _planogramRepository; // <--- Cambiado a private
        private readonly IValidateOrdersService _validateOrdersService;

        public UpdateStatusPlanogramHandler(
            IPlanogramRepository planogramRepository,
            IValidateOrdersService validateOrdersService)
        {
            _planogramRepository = planogramRepository;
            _validateOrdersService = validateOrdersService;
        }


        public async Task<bool> Handle(UpdateStatusPlanogramCommand command, CancellationToken cancellationToken)
        {
            var planogram = await _planogramRepository.GetPlanogramById(command.Id);
            if (planogram == null)
                return false;

            // Revisamos si el planograma actual tiene órdenes asociadas
            bool hasOrders = await _validateOrdersService.HasOrdersInPlanogramAsync(planogram.Id);

            if (hasOrders)
            {
                // Lanzamos la excepción que tu Middleware atrapará
                throw new Exception("No se puede editar este planograma porque ya tiene órdenes asociadas. Debe dejarlo como está.");
            }

            // Update planogram properties
            planogram.Update(command.Name, command.Description);
            await _planogramRepository.UpdateAsync(planogram);
            return true;
        }
    }
}