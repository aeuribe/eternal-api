namespace eternal_api.Application.Invoices.Interfaces
{
    public interface IInvoiceUnitOfWork : IDisposable
    {
        IInvoiceRepository InvoiceRepository { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
