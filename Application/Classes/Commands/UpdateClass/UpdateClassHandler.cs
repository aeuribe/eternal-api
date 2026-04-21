using eternal_api.Application.Classes.Interfaces;
using MediatR;

namespace eternal_api.Application.Classes.Commands.UpdateClass
{
    public class UpdateClassHandler : IRequestHandler<UpdateClassCommand, bool>
    {
        private readonly IClassRepository _classRepository;

        public UpdateClassHandler(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<bool> Handle(UpdateClassCommand command, CancellationToken cancellationToken)
        {
            var @class = await _classRepository.GetByIdAsync(command.Id);
            if (@class is null) return false;

            @class.Name = command.Name;
            await _classRepository.UpdateAsync(@class);
            return true;
        }
    }
}
