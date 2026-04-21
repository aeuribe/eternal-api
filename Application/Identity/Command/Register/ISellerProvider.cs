namespace eternal_api.Application.Identity.Command.Register
{
    public interface ISellerProvider
    {
        Task<int> GetSellersQuantity();
        Task<bool> ExistsAsync(Guid id);
    }
}
