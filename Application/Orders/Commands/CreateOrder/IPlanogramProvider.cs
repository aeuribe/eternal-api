using eternal_api.Domain.Entities;

namespace eternal_api.Application.Orders.Commands.CreateOrder
{
    public interface IPlanogramProvider
    {
        Task<Planogram?> GetActivePlanogramAsync();
    }
}
