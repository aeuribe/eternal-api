using MediatR;

namespace eternal_api.Application.Brands.Commands.DesactivateBrand
{
    public class DesactivateBrandCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
