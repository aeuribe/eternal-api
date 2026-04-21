using Amazon.S3.Model;
using eternal_api.Application.Images.Services; // Para IStorageService
using eternal_api.Domain.Enums;
using eternal_api.Infraestructure.Persistence;
using eternal_api.Infrastructure.Persistence; // Ajusta si tu AppDbContext está en otro namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necesario para FirstOrDefaultAsync
using System.IO; // Necesario para Directory y Path

namespace eternal_api.WebAPI.Endpoints
{
    public static class UtilityEndpoints
    {
        public static void MapUtilityEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupamos todo bajo /api/utilities (o /utilities dependiendo de tu prefijo global)
            var group = routes.MapGroup("/utilities").WithTags("Utilities");

            // ---------------------------------------------------------
            // Endpoint para los Estados (USA)
            // ---------------------------------------------------------
            group.MapGet("/states", () =>
            {
                var states = Enum.GetValues<StateUsEnum>()
                    .Select(s => new
                    {
                        Id = (int)s,                // Ej: 9, 43
                        Value = s.GetPrefix(),      // Ej: "FL", "TX" 
                        Label = s.GetFullName()     // Ej: "Florida", "Texas" 
                    });

                return Results.Ok(states);
            });

            // ---------------------------------------------------------
            // SCRIPT TEMPORAL PARA ETL DE IMÁGENES (S3 + PostgreSQL)
            // NOTA: Borrar o comentar después de ejecutar exitosamente.
            // ---------------------------------------------------------
            group.MapPost("/bulk-upload-images", async (
    AppDbContext dbContext,
    IStorageService storageService) =>
            {
                var folderPath = @"C:\Users\ae_ur\Desktop\GOTAS ABSTRACTAS\Gotas pequeñas";
                if (!Directory.Exists(folderPath))
                    return Results.BadRequest($"La ruta no existe: {folderPath}");

                // ✅ Solo productos de esta presentación específica
                var presentationId = Guid.Parse("019d7332-15b3-782a-ab79-3a8fd437608d");

                var files = Directory.GetFiles(folderPath, "*.png")
                                     .OrderBy(f => f)
                                     .ToArray();

                int procesados = 0;
                int ignorados = 0;
                var errores = new List<string>();
                var detalle = new List<object>();

                // Pre-cargamos solo los productos de ESA presentación
                var productos = await dbContext.Products
                    .Where(p => p.PresentationId == presentationId)
                    .ToListAsync();

                foreach (var filePath in files)
                {
                    var fileName = Path.GetFileName(filePath);
                    var codePrefix = fileName.Split('-')[0]; // "01", "10", "101", "295", etc.

                    var product = productos.FirstOrDefault(p => p.Code == codePrefix);

                    if (product == null)
                    {
                        errores.Add($"Sin producto: {fileName} (Code: '{codePrefix}')");
                        ignorados++;
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(product.ImageFileName))
                    {
                        detalle.Add(new { archivo = fileName, estado = "ignorado - ya tiene imagen", code = product.Code });
                        ignorados++;
                        continue;
                    }

                    try
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                        await using var stream = File.OpenRead(filePath);
                        var fileKey = await storageService.UploadFileAsync(stream, uniqueFileName, "image/png");

                        product.ImageFileName = fileKey;
                        dbContext.Products.Update(product);

                        detalle.Add(new { archivo = fileName, estado = "✓ subido", code = product.Code, fileKey });
                        procesados++;
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"Error subiendo {fileName}: {ex.Message}");
                        ignorados++;
                    }
                }

                await dbContext.SaveChangesAsync();

                return Results.Ok(new
                {
                    Mensaje = "ETL completado",
                    TotalArchivos = files.Length,
                    ImagenesSubidas = procesados,
                    Ignorados = ignorados,
                    Errores = errores,
                    Detalle = detalle
                });
            });

        }
    }
}