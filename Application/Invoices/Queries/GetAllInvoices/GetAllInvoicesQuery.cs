using eternal_api.Application.Common.DTOs;
using MediatR;
namespace eternal_api.Application.Bills.Queries.GetAllBills
{
    public class GetAllInvoicesQuery: IRequest<IEnumerable<InvoiceDto>>{ }
}
