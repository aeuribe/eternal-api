using eternal_api.Domain.Entities;

namespace eternal_api.Application.Invoices.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task AddPodAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<IEnumerable<Invoice>> GetAllAsync();

        Task<string?> GetLastInvoiceNumberByRouteCodeAsync(string routeCode);
    }
}
