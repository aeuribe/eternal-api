using MediatR;
namespace eternal_api.Application.Areas.Commands.DeleteArea
{
    // Devolvemos un string para poder enviar el mensaje de error si tiene regiones asociadas
    public record DeleteAreaCommand(Guid Id) : IRequest<(bool Succeeded, string ErrorMessage)>;
}