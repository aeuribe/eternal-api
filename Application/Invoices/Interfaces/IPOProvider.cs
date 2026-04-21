namespace eternal_api.Application.Invoices.Interfaces
{
    public interface IPOProvider 
    {
        Task<string> GetPoByOrderId(Guid id);
    }
}
