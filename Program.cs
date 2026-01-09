using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.invoices.Commands.Createinvoice;
using eternal_api.Infraestructure.Persistence;
using eternal_api.Infraestructure.Repositories;
using eternal_api.WebAPI.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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


// Add services to the container.
// Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IHistPriceRepository, HistPriceRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVisitLogRepository, VisitLogRepository>();
builder.Services.AddScoped<IPlanogramRepository, PlanogramRepository>();
builder.Services.AddScoped<IDistributionRepository, DistributionRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();

// Mediator
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateInvoiceHandler).Assembly);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mapeo de endpoints
app.MapGroup("/invoice")
   .WithTags("Invoice")
   .MapInvoiceEndpoints();

app.MapGroup("/orders")
   .WithTags("Orders")
   .MapOrderEndpoints();

app.MapGroup("/products")
   .WithTags("Products")
   .MapProductEndpoints();

app.MapGroup("/cities")
   .WithTags("Cities")
   .MapCityEndpoints();

app.MapGroup("/stores")
   .WithTags("Stores")
   .MapStoreEndpoints();

app.MapGroup("/users")
   .WithTags("Users")
   .MapUserEndpoints();

app.MapGroup("/visit-logs")
   .WithTags("VisitLogs")
   .MapVisitLogEndpoints();

app.MapGroup("/histprices")
   .WithTags("History Price")
   .MapHistPriceEndpoints();

app.MapGroup("/planograms")
   .WithTags("Planograms")
   .MapPlanogramEndpoints();

app.MapGroup("/distributions")
   .WithTags("Distributions")
   .MapDistributionEndpoints();

app.MapGroup("/orderdetails")
   .WithTags("Order Details")
   .MapOrderDetailEndpoints();

app.MapGroup("/invoicedetails")
   .WithTags("Invoice Details")
   .MapInvoiceDetailEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
