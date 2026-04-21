namespace eternal_api.Application.Planograms.Commands.UpdatePlanogram
{
    public interface IValidateOrdersService
    {
        Task<bool> HasOrdersInPlanogramAsync(Guid planogramId);
    }
}
