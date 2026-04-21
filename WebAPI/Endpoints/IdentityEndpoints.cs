using eternal_api.Application.Identity.Command.AdminUpdatePassword;
using eternal_api.Application.Identity.Command.ChangePassword;
using eternal_api.Application.Identity.Command.GenerateTokenPasswordReset;
using eternal_api.Application.Identity.Command.Login;
using eternal_api.Application.Identity.Command.Register;
using eternal_api.Application.Identity.Command.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace eternal_api.WebAPI.Endpoints
{
    public static class IdentityEndpoints
    {
        public static void MapIdentityEndpoints(this IEndpointRouteBuilder routes)
        {
            // LOGIN: Es público ([AllowAnonymous] por defecto en Minimal APIs si no se indica lo contrario)
            routes.MapPost("/login", async (IMediator mediator, LoginCommand command) =>
            {
                var result = await mediator.Send(command);
                return result is not null ? Results.Ok(result) : Results.Unauthorized();
            });

            // REGISTER: Solo el Administrador puede crear nuevos usuarios
            routes.MapPost("/register", async (IMediator mediator, RegisterCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
            //.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

            // CHANGE PASSWORD: Cualquier usuario autenticado puede cambiar su clave
            routes.MapPost("/change-password", async (IMediator mediator, ChangePasswordCommand command) =>
            {
                var result = await mediator.Send(command);
                return result ? Results.Ok("Contraseña actualizada") : Results.BadRequest("No se pudo cambiar la contraseña");
            })
            .RequireAuthorization(); // Requiere Token válido, no importa el rol

            // FORGOT PASSWORD: Genera el token y envía el email vía SendGrid
            routes.MapPost("/forgot-password", [AllowAnonymous] async (IMediator mediator, GenerateTokenPasswordResetCommand command) =>
            {
                var result = await mediator.Send(command);

                // Por seguridad en la tesis, siempre devolvemos Ok. 
                // Así no revelamos si un email existe o no en la base de datos.
                return Results.Ok(new { Message = "Si el correo está registrado, recibirás un enlace de recuperación pronto." });
            });

            routes.MapPost("/admin/update-user-password", async (IMediator mediator, AdminUpdatePasswordCommand command) =>
            {
                var result = await mediator.Send(command);

                if (result.Succeeded)
                {
                    return Results.Ok(new { Message = "Contraseña de usuario actualizada exitosamente." });
                }

                // Ahora sí sabremos qué pasó
                return Results.BadRequest(new
                {
                    Message = "No se pudo actualizar la contraseña del usuario.",
                    Errors = result.Errors // <-- Aquí saldrá el texto exacto del error
                });
            });
            // .RequireAuthorization(...) // Recuerda descomentar esto en producción

            // RESET PASSWORD: El paso final que consume el token y cambia la clave
            routes.MapPost("/reset-password", [AllowAnonymous] async (IMediator mediator, ResetPasswordCommand command) =>
            {
                var result = await mediator.Send(command);

                if (result.Succeeded)
                {
                    return Results.Ok(new { Message = "¡Contraseña actualizada! Ya puedes iniciar sesión en Order It." });
                }

                return Results.BadRequest(new
                {
                    Message = "No se pudo restablecer la contraseña",
                    Errors = result.Errors
                });
            })
            .AllowAnonymous();
        }
    }
}
