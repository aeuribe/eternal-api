using eternal_api.Domain.Entities;

namespace eternal_api.Application.Invoices.Commands.CreateInvoice
{
    public interface IInvoiceSellerProvider
    {
        Task<User> GetUserByOrderId(Guid id);
    }
}
