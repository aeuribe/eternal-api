namespace eternal_api.Application.Orders.Queries.GetOrdersByStoreId
{
    public interface IStoreValidationService
    {
        Task<bool> ExistsAsync(Guid storeId);
    }
}
