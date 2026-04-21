using MediatR;

namespace eternal_api.Application.Areas.Commands.UpdateArea
{
    public class UpdateAreaHandler : IRequestHandler<UpdateAreaCommand, bool>
    {
        private readonly IAreaRepository _repository;
        public UpdateAreaHandler(IAreaRepository repository) => _repository = repository;

        public async Task<bool> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(request.Id);
            if (area == null) return false;

            area.Update(request.Name);
            await _repository.UpdateAsync(area);
            return true;
        }
    }
}