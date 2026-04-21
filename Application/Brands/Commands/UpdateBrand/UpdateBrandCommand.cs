using MediatR;

namespace eternal_api.Application.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
