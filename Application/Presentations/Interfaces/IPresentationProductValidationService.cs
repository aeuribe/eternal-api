namespace eternal_api.Application.Presentations.Interfaces
{
    public interface IPresentationProductValidationService
    {
        Task<bool> HasAnyProductAssociatedAsync(Guid presentationId);
        Task<bool> HasAnyProductWithOrdersAssociatedAsync(Guid presentationId);
    }
}
