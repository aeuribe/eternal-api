namespace eternal_api.Application.Orders.Queries.GetOrdersBySalespersonId
{
    public interface ISalespersonValidationService
    {
        Task<bool> ExistsAsync(Guid userId);
    }
}
