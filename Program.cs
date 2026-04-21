using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using eternal_api.Application.Assignments.Interfaces;
using eternal_api.Application.Brands.Interfaces;
using eternal_api.Application.Cities.Interfaces;
using eternal_api.Application.Classes.Interfaces;
using eternal_api.Application.Distributions.Interfaces;
using eternal_api.Application.Districts.Interfaces;
using eternal_api.Application.Families.Interfaces;
using eternal_api.Application.Identity.Command.Login;
using eternal_api.Application.Identity.Command.Register;
using eternal_api.Application.Identity.Interfaces;
using eternal_api.Application.Identity.Services;
using eternal_api.Application.Images.Services;
using eternal_api.Application.Invoices.Commands.CreateInvoice;
using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Orders.Commands.CreateOrder;
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.Orders.Queries.GetOrdersBySalespersonId;
using eternal_api.Application.Orders.Queries.GetOrdersByStoreId;
using eternal_api.Application.Planograms.Commands.UpdatePlanogram;
using eternal_api.Application.Planograms.Interfaces;
using eternal_api.Application.Presentations.Interfaces;
using eternal_api.Application.Prices.Interfaces;
using eternal_api.Application.Products.Interfaces;
using eternal_api.Application.Regions.Interfaces;
using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Application.Stores.Interfaces;
using eternal_api.Application.Users.Interfaces;
using eternal_api.Infraestructure.Identity;
using eternal_api.Infraestructure.Persistence;
using eternal_api.Infraestructure.Persistence.Seeders;
using eternal_api.Infraestructure.Repositories;
using eternal_api.Infraestructure.Services;
using eternal_api.Infrastructure.Services;
using eternal_api.WebAPI.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Leer la ConnectionString
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Reemplazar el placeholder con la variable de entorno
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
if (string.IsNullOrEmpty(password))
{
    throw new Exception("La variable de entorno DB_PASSWORD no está definida.");
}
connString = connString.Replace("{DB_PASSWORD}", password);

// Agregar DbContext con PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

// Identity DbContext
builder.Services.AddDbContext<SecurityDbContext>(options => 
    options.UseNpgsql(connString));


// Add services to the container.
// Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IHistPriceRepository, HistPriceRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IPlanogramRepository, PlanogramRepository>();
builder.Services.AddScoped<IDistributionRepository, DistributionRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IPresentationRepository, PresentationRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();

builder.Services.AddScoped<IStoreValidationService, StoreRepository>();
builder.Services.AddScoped<ISalespersonValidationService, UserRepository>();
builder.Services.AddScoped<IUserIdentityProvider, UserRepository>();
builder.Services.AddScoped<IUserRegistrationService, UserRepository>();
builder.Services.AddScoped<IValidateOrdersService, DistributionRepository>();
builder.Services.AddScoped<IOrderValidationService, OrderRepository>();
builder.Services.AddScoped<IPlanogramProvider, PlanogramRepository>();
builder.Services.AddScoped<IInvoiceProvider, InvoiceRepository>();
builder.Services.AddScoped<IOrderUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInvoiceUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPOProvider, OrderRepository>();
builder.Services.AddScoped<ISellerProvider, UserRepository>();
builder.Services.AddScoped<ICityProvider, CityRepository>();
builder.Services.AddScoped<IInvoiceSellerProvider, OrderRepository>();
builder.Services.AddScoped<ISalesRouteRepository, SalesRouteRepository>();
builder.Services.AddScoped<IPresentationProductValidationService, ProductRepository>();
builder.Services.AddScoped<IClassFamilyValidationService, FamilyRepository>();


// Registra la interfaz con su implementación real
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<TokenService>();

//Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SecurityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();

// Mediator
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateInvoiceHandler).Assembly);
});

//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddControllers(options =>
{
    // Elimina cualquier filtro global de AutoValidateAntiforgeryTokenAttribute
    var antiforgeryFilters = options.Filters
        .Where(f => f is AutoValidateAntiforgeryTokenAttribute)
        .ToList();

    foreach (var f in antiforgeryFilters)
    {
        options.Filters.Remove(f);
    }
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Eternal API", Version = "v1" });

    // Definir el esquema de seguridad JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingresa el token JWT únicamente. Ejemplo: 12345abcdef"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 1. DEFINIR LA POLÍTICA CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true) // <--- IMPORTANTE: Permite cualquier IP
           .AllowCredentials(); // Necesario si envías tokens o cookies
    });
});

// SERVICIO DE AWS
// 1. Leer configuración AWS
var awsOptions = builder.Configuration.GetSection("AWS");
var accessKey = awsOptions["AccessKey"];
var secretKey = awsOptions["SecretKey"];
var region = awsOptions["Region"];

// 2. Crear credenciales
var credentials = new BasicAWSCredentials(accessKey, secretKey);

// 3. Registrar AmazonS3Client
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    return new AmazonS3Client(credentials, RegionEndpoint.GetBySystemName(region));
});

// 4. Registrar tu servicio de almacenamiento
builder.Services.AddScoped<IStorageService, S3StorageService>();



var app = builder.Build();

// Crear roles por defecto al iniciar
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = new[] { "Admin", "Seller" };

    foreach (var role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}


//app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

app.UseAuthentication(); // <-- AGREGAR ESTO (Esencial para JWT)
app.UseAuthorization();


// Mapeo de endpoints
app.MapGroup("/invoice")
   .WithTags("Invoice")
   .RequireAuthorization()
   .MapInvoiceEndpoints();

app.MapGroup("/orders")
   .WithTags("Orders")
   .RequireAuthorization()
   .MapOrderEndpoints();

app.MapGroup("/products")
   .WithTags("Products")
   .RequireAuthorization()
   .MapProductEndpoints();

app.MapGroup("/cities")
   .WithTags("Cities")
   .RequireAuthorization()
   .MapCityEndpoints();

app.MapGroup("/stores")
   .WithTags("Stores")
   .RequireAuthorization()
   .MapStoreEndpoints();

app.MapGroup("/users")
   .WithTags("Users")
   .RequireAuthorization()
   .MapUserEndpoints();

app.MapGroup("/assignments")
   .WithTags("Assignments")
   .RequireAuthorization()
   .MapAssignmentEndpoints();

app.MapGroup("/histprices")
   .WithTags("History Price")
   .RequireAuthorization()
   .MapHistPriceEndpoints();

app.MapGroup("/planograms")
   .WithTags("Planograms")
   .RequireAuthorization()
   .MapPlanogramEndpoints();

app.MapGroup("/distributions")
   .WithTags("Distributions")
   .RequireAuthorization()
   .MapDistributionEndpoints();

app.MapGroup("/brands")
   .WithTags("Brands")
   .MapBrandEndpoints();

app.MapGroup("/families")
   .WithTags("Families")
   .MapFamilyEndpoints();

app.MapGroup("/presentations")
   .WithTags("Presentations")
   .MapPresentationEndpoints();

app.MapGroup("/classes")
   .WithTags("Classes")
   .MapClassEndpoints();

app.MapGroup("/auth")
   .WithTags("Identity")
   .MapIdentityEndpoints();

app.MapGroup("/images")
   .WithTags("Images")
   .MapStorageEndpoints();

app.MapGroup("/salesRoutes")
    .WithTags("SalesRoutes")
    .MapSalesRouteEndpoints();

app.MapGroup("/areas")
    .WithTags("Areas")
    .MapAreaEndpoints();

app.MapGroup("/regions")
    .WithTags("Regions")
    .MapRegionEndpoints();

app.MapGroup("/districts")
    .WithTags("Districts")
    .MapDistrictEndpoints();

app.MapUtilityEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Seeders
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Aquí podrías agregar logs o migraciones automáticas si quisieras
    await CitySeeder.SeedAsync(context);

}

using (var scope = app.Services.CreateScope())  
{
    var services = scope.ServiceProvider;

    // Asegúrate de que Identity y RoleManager estén registrados en DI
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Seed de roles
    await RoleSeeder.SeedAsync(roleManager);
}



app.Run();
