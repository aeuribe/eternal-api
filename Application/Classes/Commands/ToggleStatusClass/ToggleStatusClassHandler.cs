using eternal_api.Application.Classes.Interfaces;
using MediatR;

namespace eternal_api.Application.Classes.Commands.ToggleStatusClass
{
    public class ToggleStatusClassHandler : IRequestHandler<ToggleStatusClassCommand, bool>
    {
        private readonly IClassRepository _classRepository;

        public ToggleStatusClassHandler(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<bool> Handle(ToggleStatusClassCommand command, CancellationToken cancellationToken)
        {
            var @class = await _classRepository.GetByIdAsync(command.Id);
            if (@class is null) return false;

            @class.ToggleStatus();
            await _classRepository.UpdateAsync(@class);
            return true;
        }
    }
}
