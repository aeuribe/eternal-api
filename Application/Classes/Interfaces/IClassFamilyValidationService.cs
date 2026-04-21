namespace eternal_api.Application.Classes.Interfaces
{
    public interface IClassFamilyValidationService
    {
        Task<bool> HasAnyFamilyAssociatedAsync(Guid classId);
    }
}
