using eternal_api.Application.Invoices.Queries.DTOs;
using MediatR;
namespace eternal_api.Application.Bills.Queries.GetAllBills
{
    public class GetAllInvoicesQuery: IRequest<IEnumerable<InvoiceDto>>{ }
}
