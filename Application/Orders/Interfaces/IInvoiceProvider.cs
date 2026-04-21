using eternal_api.Domain.Entities;

namespace eternal_api.Application.Orders.Interfaces
{
    public interface IInvoiceProvider
    {
        Task<Invoice?> GetInvoiceByOrderIdAsync(Guid id);
    }
}
