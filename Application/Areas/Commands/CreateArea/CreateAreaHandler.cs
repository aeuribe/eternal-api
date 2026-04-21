using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Areas.Commands.CreateArea
{
    public class CreateAreaHandler : IRequestHandler<CreateAreaCommand, Guid>
    {
        private readonly IAreaRepository _repository;
        public CreateAreaHandler(IAreaRepository repository) => _repository = repository;

        public async Task<Guid> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            var area = new Area(request.Name);
            await _repository.AddAsync(area);
            return area.Id;
        }
    }
}