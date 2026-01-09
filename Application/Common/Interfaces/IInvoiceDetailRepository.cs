using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        Task AddAsync(InvoiceDetail invoiceDetail);
        Task UpdateAsync(InvoiceDetail invoiceDetail);
        Task<InvoiceDetail?> GetByIdAsync(Guid id);
        Task<IEnumerable<InvoiceDetail?>> GetAllInvoiceDetailsByInvoiceIdAsync(Guid invoiceId);
        Task<bool> ExistsAsync(Guid id);
    }
}
