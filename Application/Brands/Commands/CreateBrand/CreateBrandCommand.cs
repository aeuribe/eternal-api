using MediatR;

namespace eternal_api.Application.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
