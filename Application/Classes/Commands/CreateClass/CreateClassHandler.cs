using eternal_api.Application.Classes.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Classes.Commands.CreateClass
{
    public class CreateClassHandler : IRequestHandler<CreateClassCommand, Guid>
    {
        private readonly IClassRepository _classRepository;

        public CreateClassHandler(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<Guid> Handle(CreateClassCommand command, CancellationToken cancellationToken)
        {
            var existing = await _classRepository.GetByNameAsync(command.Name);
            if (existing is not null)
            {
                throw new InvalidOperationException($"Ya existe una clase con el nombre '{command.Name}'.");
            }

            var @class = new Class(command.Name);
            await _classRepository.AddAsync(@class);
            return @class.Id;
        }
    }
}
